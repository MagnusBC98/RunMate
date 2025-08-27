"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import RunRequestCard from "@/app/components/RunRequestCard";

type RunRequest = {
  id: string;
  requesterUserId: string;
  requesterFirstName: string;
  requesterLastName: string;
  status: string;
};

export default function RunRequestsPage() {
  const [requests, setRequests] = useState<RunRequest[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const params = useParams();
  const router = useRouter();
  const runId = params.runId;

  useEffect(() => {
    const fetchRunRequests = async () => {
      const token = localStorage.getItem("authToken");
      if (!token || !runId) {
        router.push("/login");
        return;
      }

      try {
        const response = await fetch(
          `https://localhost:7251/api/runs/${runId}/requests`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (!response.ok) {
          throw new Error("Failed to fetch run requests.");
        }

        const data: RunRequest[] = await response.json();
        setRequests(data);
      } catch (error) {
        console.error(error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchRunRequests();
  }, [runId, router]);

  const handleStatusChange = (requestId: string, newStatus: string) => {
    setRequests((currentRequests) =>
      currentRequests.map((req) =>
        req.id === requestId ? { ...req, status: newStatus } : req
      )
    );
  };

  if (isLoading)
    return <div className="p-8 text-center">Loading requests...</div>;

  return (
    <main className="flex flex-col items-center p-8">
      <div className="w-full max-w-2xl">
        <h1 className="mb-6 text-3xl font-bold">Incoming Requests</h1>

        {requests.length === 0 ? (
          <p className="text-gray-400">No requests for this run yet.</p>
        ) : (
          <div className="space-y-4">
            {requests.map((request) => (
              <RunRequestCard
                key={request.id}
                request={request}
                onStatusChange={handleStatusChange}
              />
            ))}
          </div>
        )}
      </div>
    </main>
  );
}
