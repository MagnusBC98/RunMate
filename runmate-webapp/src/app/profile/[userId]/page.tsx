"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";

type UserProfile = {
  id: string;
  firstName: string;
  lastName: string;
};

type UserStats = {
  fiveKmPb: string | null;
  tenKmPb: string | null;
  halfMarathonPb: string | null;
  marathonPb: string | null;
};

export default function ProfilePage() {
  const [profile, setProfile] = useState<UserProfile | null>(null);
  const [stats, setStats] = useState<UserStats | null>(null);

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [fiveKmPb, setFiveKmPb] = useState("");
  const [tenKmPb, setTenKmPb] = useState("");
  const [halfMarathonPb, setHalfMarathonPb] = useState("");
  const [marathonPb, setMarathonPb] = useState("");

  const [isOwnProfile, setIsOwnProfile] = useState(false);
  const [activeTab, setActiveTab] = useState("info");
  const [isLoading, setIsLoading] = useState(true);
  const [message, setMessage] = useState<{
    type: "success" | "error";
    text: string;
  } | null>(null);

  const params = useParams();
  const router = useRouter();

  useEffect(() => {
    const fetchProfileData = async () => {
      const token = localStorage.getItem("authToken");
      if (!token) {
        router.push("/login");
        return;
      }

      const myUserId = localStorage.getItem("userId");
      let userIdToFetch = params.userId;

      if (params.userId === "me") {
        if (!myUserId) {
          router.push("/login");
          return;
        }
        userIdToFetch = myUserId;
        setIsOwnProfile(true);
      } else {
        setIsOwnProfile(params.userId === myUserId);
      }

      try {
        const [profileRes, statsRes] = await Promise.all([
          fetch(`https://localhost:7251/api/users/${userIdToFetch}`, {
            headers: { Authorization: `Bearer ${token}` },
          }),
          fetch(`https://localhost:7251/api/users/${userIdToFetch}/stats`, {
            headers: { Authorization: `Bearer ${token}` },
          }),
        ]);

        if (!profileRes.ok || !statsRes.ok)
          throw new Error("Failed to fetch profile data.");

        const profileData: UserProfile = await profileRes.json();
        const statsData: UserStats = await statsRes.json();

        setProfile(profileData);
        setStats(statsData);

        // Populate form state with fetched data
        setFirstName(profileData.firstName);
        setLastName(profileData.lastName);
        setFiveKmPb(statsData.fiveKmPb || "");
        setTenKmPb(statsData.tenKmPb || "");
        setHalfMarathonPb(statsData.halfMarathonPb || "");
        setMarathonPb(statsData.marathonPb || "");
      } catch (error) {
        console.error(error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchProfileData();
  }, [params.userId, router]);

  const handleSave = async () => {
    setMessage(null);
    const token = localStorage.getItem("authToken");
    if (!isOwnProfile || !token) return;

    try {
      const [profileUpdateRes, statsUpdateRes] = await Promise.all([
        fetch("https://localhost:7251/api/users/me", {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify({ firstName, lastName }),
        }),
        fetch("https://localhost:7251/api/users/me/stats", {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify({
            fiveKmPb,
            tenKmPb,
            halfMarathonPb,
            marathonPb,
          }),
        }),
      ]);

      if (!profileUpdateRes.ok || !statsUpdateRes.ok)
        throw new Error("Failed to save changes.");

      setMessage({ type: "success", text: "Profile updated successfully!" });
    } catch (error) {
      setMessage({ type: "error", text: "An error occurred while saving." });
      console.error(error);
    }
  };

  if (isLoading)
    return <div className="p-8 text-center">Loading profile...</div>;
  if (!profile || !stats)
    return <div className="p-8 text-center">Could not load profile.</div>;

  return (
    <div className="flex flex-col items-center p-8">
      <div className="w-full max-w-2xl">
        <h1 className="mb-6 text-3xl font-bold">
          {isOwnProfile
            ? "My Profile"
            : `${profile.firstName} ${profile.lastName}'s Profile`}
        </h1>

        <div className="mb-6 flex border-b border-gray-700">
          <button
            onClick={() => setActiveTab("info")}
            className={`px-4 py-2 ${
              activeTab === "info"
                ? "border-b-2 border-blue-500 text-white"
                : "text-gray-400"
            }`}
          >
            User Info
          </button>
          <button
            onClick={() => setActiveTab("stats")}
            className={`px-4 py-2 ${
              activeTab === "stats"
                ? "border-b-2 border-blue-500 text-white"
                : "text-gray-400"
            }`}
          >
            Running Stats
          </button>
        </div>

        <div>
          {activeTab === "info" && (
            <div className="space-y-4">
              <div>
                <label className="text-sm text-gray-400">First Name</label>
                <input
                  type="text"
                  value={firstName}
                  onChange={(e) => setFirstName(e.target.value)}
                  readOnly={!isOwnProfile}
                  className="input-field mt-1 bg-gray-800 read-only:bg-gray-700 read-only:cursor-not-allowed"
                />
              </div>
              <div>
                <label className="text-sm text-gray-400">Last Name</label>
                <input
                  type="text"
                  value={lastName}
                  onChange={(e) => setLastName(e.target.value)}
                  readOnly={!isOwnProfile}
                  className="input-field mt-1 bg-gray-800 read-only:bg-gray-700 read-only:cursor-not-allowed"
                />
              </div>
            </div>
          )}

          {activeTab === "stats" && (
            <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
              <div>
                <label className="text-sm text-gray-400">5k PB</label>
                <input
                  type="text"
                  value={fiveKmPb}
                  onChange={(e) => setFiveKmPb(e.target.value)}
                  readOnly={!isOwnProfile}
                  className="input-field mt-1 bg-gray-800 read-only:bg-gray-700 read-only:cursor-not-allowed"
                  placeholder="hh:mm:ss"
                />
              </div>
              <div>
                <label className="text-sm text-gray-400">10k PB</label>
                <input
                  type="text"
                  value={tenKmPb}
                  onChange={(e) => setTenKmPb(e.target.value)}
                  readOnly={!isOwnProfile}
                  className="input-field mt-1 bg-gray-800 read-only:bg-gray-700 read-only:cursor-not-allowed"
                  placeholder="hh:mm:ss"
                />
              </div>
              <div>
                <label className="text-sm text-gray-400">
                  Half Marathon PB
                </label>
                <input
                  type="text"
                  value={halfMarathonPb}
                  onChange={(e) => setHalfMarathonPb(e.target.value)}
                  readOnly={!isOwnProfile}
                  className="input-field mt-1 bg-gray-800 read-only:bg-gray-700 read-only:cursor-not-allowed"
                  placeholder="hh:mm:ss"
                />
              </div>
              <div>
                <label className="text-sm text-gray-400">Marathon PB</label>
                <input
                  type="text"
                  value={marathonPb}
                  onChange={(e) => setMarathonPb(e.target.value)}
                  readOnly={!isOwnProfile}
                  className="input-field mt-1 bg-gray-800 read-only:bg-gray-700 read-only:cursor-not-allowed"
                  placeholder="hh:mm:ss"
                />
              </div>
            </div>
          )}
        </div>

        {isOwnProfile && (
          <div className="mt-8">
            <button
              onClick={handleSave}
              className="rounded bg-blue-600 px-6 py-3 font-bold text-white transition hover:bg-blue-700"
            >
              Save Changes
            </button>
            {message && (
              <p
                className={`mt-4 text-sm ${
                  message.type === "success" ? "text-green-500" : "text-red-500"
                }`}
              >
                {message.text}
              </p>
            )}
          </div>
        )}
      </div>
    </div>
  );
}
