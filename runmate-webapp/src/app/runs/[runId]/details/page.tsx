"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import Link from "next/link";

type RunDetails = {
  id: string;
  runDate: string;
  distanceInKm: number;
  avgPaceInMinutesPerKm: string;
  userId: string;
};

export default function RunDetailsPage() {
  const [run, setRun] = useState<RunDetails | null>(null);
  const [isOwner, setIsOwner] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);

  const [runDate, setRunDate] = useState("");
  const [distanceInKm, setDistanceInKm] = useState("");
  const [avgPace, setAvgPace] = useState("");

  const params = useParams();
  const router = useRouter();
  const runId = params.runId;

  useEffect(() => {
    const fetchRunDetails = async () => {
      const token = localStorage.getItem("authToken");
      const currentUserId = localStorage.getItem("userId");
      if (!token || !currentUserId) {
        router.push("/login");
        return;
      }

      try {
        const response = await fetch(
          `https://localhost:7251/api/runs/${runId}`,
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        );
        if (!response.ok) throw new Error("Failed to fetch run details.");

        const data: RunDetails = await response.json();
        setRun(data);
        setIsOwner(data.userId === currentUserId);
        setRunDate(new Date(data.runDate).toISOString().substring(0, 10));
        setDistanceInKm(data.distanceInKm.toString());
        setAvgPace(data.avgPaceInMinutesPerKm);
      } catch (err: any) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    };
    if (runId) {
      fetchRunDetails();
    }
  }, [runId, router]);

  const handleUpdate = async () => {
    if (!isOwner) return;
    const token = localStorage.getItem("authToken");
    setMessage(null);
    setError(null);

    try {
      const response = await fetch(`https://localhost:7251/api/runs/${runId}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          runDate: new Date(runDate),
          distanceInKm: parseFloat(distanceInKm),
          avgPaceInMinutesPerKm: avgPace,
        }),
      });

      if (!response.ok) throw new Error("Failed to update run.");

      setMessage("Run updated successfully!");
    } catch (err: any) {
      setError(err.message);
    }
  };

  const handleDelete = async () => {
    if (!isOwner) return;
    const token = localStorage.getItem("authToken");
    if (window.confirm("Are you sure you want to delete this run?")) {
      try {
        const response = await fetch(
          `https://localhost:7251/api/runs/${runId}`,
          {
            method: "DELETE",
            headers: { Authorization: `Bearer ${token}` },
          }
        );
        if (!response.ok) throw new Error("Failed to delete run.");

        router.push("/runs/my-runs");
      } catch (err: any) {
        setError(err.message);
      }
    }
  };

  const handleMakeRequest = async () => {
    if (isOwner) return;
    const token = localStorage.getItem("authToken");
    try {
      const response = await fetch(
        `https://localhost:7251/api/runs/${runId}/request`,
        {
          method: "POST",
          headers: { Authorization: `Bearer ${token}` },
        }
      );
      if (!response.ok) throw new Error("Failed to send request.");

      alert("Run request sent successfully!");
    } catch (err: any) {
      setError(err.message);
    }
  };

  if (isLoading)
    return <div className="p-8 text-center">Loading run details...</div>;
  if (error) return <div className="p-8 text-center text-red-500">{error}</div>;
  if (!run) return <div className="p-8 text-center">Run not found.</div>;

  return (
    <main className="flex flex-col items-center p-8">
      <div className="w-full max-w-lg">
        <h1 className="mb-6 text-3xl font-bold">Run Details</h1>
        <div className="rounded-lg bg-gray-800 p-8 shadow-md">
          {/* Form fields for run data */}
          <div className="mb-4">
            <label className="mb-2 block text-sm font-bold text-gray-400">
              Run Date
            </label>
            <input
              type="date"
              value={runDate}
              onChange={(e) => setRunDate(e.target.value)}
              readOnly={!isOwner}
              className="input-field"
            />
          </div>
          <div className="mb-4">
            <label className="mb-2 block text-sm font-bold text-gray-400">
              Distance (km)
            </label>
            <input
              type="number"
              value={distanceInKm}
              onChange={(e) => setDistanceInKm(e.target.value)}
              readOnly={!isOwner}
              className="input-field"
            />
          </div>
          <div className="mb-6">
            <label className="mb-2 block text-sm font-bold text-gray-400">
              Average Pace (per km)
            </label>
            <input
              type="text"
              value={avgPace}
              onChange={(e) => setAvgPace(e.target.value)}
              readOnly={!isOwner}
              className="input-field"
            />
          </div>

          {!isOwner && (
            <div className="mb-6 text-sm text-gray-400">
              Posted by:
              <Link
                href={`/profile/${run.userId}`}
                className="ml-2 font-bold text-blue-400 hover:underline"
              >
                View Profile
              </Link>
            </div>
          )}

          {message && (
            <p className="mt-4 text-center text-green-500">{message}</p>
          )}
          {error && <p className="mt-4 text-center text-red-500">{error}</p>}

          {/* Conditional Buttons */}
          <div className="mt-8 flex flex-col space-y-4">
            {isOwner ? (
              <>
                <button
                  onClick={handleUpdate}
                  className="w-full rounded bg-blue-600 px-4 py-3 font-bold text-white transition hover:bg-blue-700"
                >
                  Update Run
                </button>

                <Link
                  href={`/runs/${run.id}/requests`}
                  className="w-full text-center rounded bg-gray-600 px-4 py-3 font-bold text-white transition hover:bg-gray-500"
                >
                  View Requests
                </Link>
                <button
                  onClick={handleDelete}
                  className="w-full rounded bg-red-600 px-4 py-3 font-bold text-white transition hover:bg-red-700"
                >
                  Delete Run
                </button>
              </>
            ) : (
              <button
                onClick={handleMakeRequest}
                className="w-full rounded bg-green-600 px-4 py-3 font-bold text-white transition hover:bg-green-700"
              >
                Make Request
              </button>
            )}
          </div>
        </div>
      </div>
    </main>
  );
}
