"use client";

import { useEffect, useState } from "react";
import { useSearchParams, useRouter } from "next/navigation";
import RunCard from "@/app/components/RunCard";

type Run = {
  id: string;
  runDate: string;
  distanceInKm: number;
  avgPaceInMinutesPerKm: string;
};

export default function SearchResultsPage() {
  const [runs, setRuns] = useState<Run[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const searchParams = useSearchParams();
  const router = useRouter();

  useEffect(() => {
    const fetchSearchResults = async () => {
      const token = localStorage.getItem("authToken");
      if (!token) {
        router.push("/login");
        return;
      }

      const query = new URLSearchParams({
        minDistanceKm: searchParams.get("minDistance") || "",
        maxDistanceKm: searchParams.get("maxDistance") || "",
        minPace: searchParams.get("minPace") || "",
        maxPace: searchParams.get("maxPace") || "",
      }).toString();

      try {
        const response = await fetch(
          `https://localhost:7251/api/runs?${query}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (!response.ok) {
          throw new Error("Failed to fetch search results.");
        }

        const data: Run[] = await response.json();
        setRuns(data);
      } catch (error) {
        console.error(error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchSearchResults();
  }, [searchParams, router]);

  if (isLoading)
    return <div className="p-8 text-center">Searching for runs...</div>;

  return (
    <main className="flex flex-col items-center p-8">
      <div className="w-full max-w-2xl">
        <h1 className="mb-6 text-3xl font-bold">Search Results</h1>

        {runs.length === 0 ? (
          <p className="text-gray-400">No runs found matching your criteria.</p>
        ) : (
          <div className="w-full">
            {runs.map((run) => (
              <RunCard key={run.id} run={run} />
            ))}
          </div>
        )}
      </div>
    </main>
  );
}
