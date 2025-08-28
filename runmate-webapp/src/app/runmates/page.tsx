"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import RunMateCard from "../components/RunMateCard";
import { RunMate } from "../types/runmate";

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

  const handleUnmatch = async (requestId: string): Promise<boolean> => {
    const token = localStorage.getItem("authToken");
    try {
      const response = await fetch(
        `https://localhost:7251/api/run-requests/${requestId}`,
        {
          method: "PATCH",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify({ status: "Declined" }),
        }
      );
      if (!response.ok) throw new Error("Failed to unmatch.");

      setRunMates((currentMates) =>
        currentMates.filter((mate) => mate.requestId !== requestId)
      );
      return true;
    } catch (err: any) {
      alert(err.message);
      return false;
    }
  };

  if (isLoading)
    return <div className="p-8 text-center">Loading your RunMates...</div>;

  return (
    <main className="flex flex-col items-center p-8">
      <div className="w-full max-w-2xl">
        <h1 className="mb-6 text-3xl font-bold">My RunMates</h1>

        {runMates.length === 0 ? (
          <p>You haven't matched with any runners yet.</p>
        ) : (
          <div className="space-y-4">
            {runMates.map((mate) => (
              <RunMateCard
                key={mate.requestId}
                mate={mate}
                onUnmatch={handleUnmatch}
              />
            ))}
          </div>
        )}
      </div>
    </main>
  );
}
