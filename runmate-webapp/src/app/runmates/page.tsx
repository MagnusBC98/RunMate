"use client";

import { useEffect, useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";

type RunMate = {
  runId: string;
  runDate: string;
  runMateUserName: string;
  runMateUserId: string;
};

export default function MyRunMatesPage() {
  const [runMates, setRunMates] = useState<RunMate[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    const fetchRunMates = async () => {
      const token = localStorage.getItem("authToken");
      if (!token) {
        router.push("/login");
        return;
      }

      try {
        const response = await fetch("https://localhost:7251/api/runmates", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (!response.ok) {
          throw new Error("Failed to fetch run-mates.");
        }

        const data: RunMate[] = await response.json();
        setRunMates(data);
      } catch (error) {
        console.error(error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchRunMates();
  }, [router]);

  if (isLoading)
    return <div className="p-8 text-center">Loading your RunMates...</div>;

  return (
    <main className="flex flex-col items-center p-8">
      <div className="w-full max-w-2xl">
        <h1 className="mb-6 text-3xl font-bold">My RunMates</h1>

        {runMates.length === 0 ? (
          <p className="text-gray-400">
            You haven't matched with any runners yet.
          </p>
        ) : (
          <div className="space-y-4">
            {runMates.map((mate) => (
              <div
                key={mate.runId}
                className="rounded-lg bg-gray-800 p-4 flex items-center space-x-4"
              >
                {" "}
                <div className="flex-shrink-0">
                  <img
                    src="/logo-cropped.jpg"
                    alt="RunMate Logo"
                    className="h-12 w-12 object-fill rounded-full "
                  />
                </div>
                <div>
                  {" "}
                  <p className="font-bold text-lg">
                    Run on:{" "}
                    {new Date(mate.runDate).toLocaleDateString("en-GB", {
                      year: "numeric",
                      month: "long",
                      day: "numeric",
                    })}
                  </p>
                  <div className="mt-2 text-gray-300">
                    <span>With: </span>
                    <Link href={`/profile/${mate.runMateUserId}`}>
                      <span className="font-bold text-white transition hover:text-blue-400 hover:underline">
                        {mate.runMateUserName}
                      </span>
                    </Link>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </main>
  );
}
