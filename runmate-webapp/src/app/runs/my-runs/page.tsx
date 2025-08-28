"use client";

import { useEffect, useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import RunCard from "@/app/components/RunCard"; // Import the new component

// 1. Define a type for a single Run object
type Run = {
  id: string;
  runDate: string;
  distanceInKm: number;
  avgPaceInMinutesPerKm: string;
};

export default function MyRunsPage() {
  const [runs, setRuns] = useState<Run[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    const fetchMyRuns = async () => {
      const token = localStorage.getItem("authToken");
      if (!token) {
        router.push("/login");
        return;
      }

      try {
        const response = await fetch("https://localhost:7251/api/runs/me", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (!response.ok) {
          throw new Error("Failed to fetch runs.");
        }

        const data: Run[] = await response.json();
        setRuns(data);
      } catch (error) {
        console.error(error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchMyRuns();
  }, [router]);

  if (isLoading)
    return <div className="p-8 text-center">Loading your runs...</div>;

  return (
    <main className="flex flex-col items-center p-8">
      <div className="w-full max-w-2xl">
        <h1 className="mb-6 text-3xl font-bold">My Runs</h1>

        {runs.length === 0 ? (
          <p className="text-gray-400">You haven't logged any runs yet.</p>
        ) : (
          <div>
            {runs.map((run) => (
              <RunCard key={run.id} run={run} isClickable={true} />
            ))}
          </div>
        )}
      </div>
    </main>
  );
}
