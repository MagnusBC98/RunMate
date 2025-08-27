"use client";

import Link from "next/link";
import { useState } from "react";

type RunRequest = {
  id: string;
  requesterUserId: string;
  requesterFirstName: string;
  requesterLastName: string;
  status: string;
};

type RunRequestCardProps = {
  request: RunRequest;
  onStatusChange: (requestId: string, newStatus: string) => void;
};

export default function RunRequestCard({
  request,
  onStatusChange,
}: RunRequestCardProps) {
  const [isLoading, setIsLoading] = useState(false);

  const handleUpdateStatus = async (newStatus: "Accepted" | "Declined") => {
    setIsLoading(true);
    const token = localStorage.getItem("authToken");

    try {
      const response = await fetch(
        `https://localhost:7251/api/run-requests/${request.id}`,
        {
          method: "PATCH",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify({ status: newStatus }),
        }
      );

      if (!response.ok) throw new Error("Failed to update status.");

      onStatusChange(request.id, newStatus);
    } catch (error) {
      console.error(error);
      alert("Failed to update request status.");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="rounded-lg bg-gray-800 p-4">
      <div className="flex items-center justify-between">
        <div>
          <p className="font-bold text-lg">
            Request from: {request.requesterFirstName}{" "}
            {request.requesterLastName}
          </p>
          <Link
            href={`/profile/${request.requesterUserId}`}
            className="text-sm text-blue-400 hover:underline"
          >
            View Profile
          </Link>
        </div>
        <p className="text-sm text-gray-400">Status: {request.status}</p>
      </div>

      {request.status === "Pending" && (
        <div className="mt-4 flex space-x-4">
          <button
            onClick={() => handleUpdateStatus("Accepted")}
            disabled={isLoading}
            className="w-full rounded bg-green-600 px-4 py-2 font-bold text-white transition hover:bg-green-700 disabled:opacity-50"
          >
            Accept
          </button>
          <button
            onClick={() => handleUpdateStatus("Declined")}
            disabled={isLoading}
            className="w-full rounded bg-red-600 px-4 py-2 font-bold text-white transition hover:bg-red-700 disabled:opacity-50"
          >
            Decline
          </button>
        </div>
      )}
    </div>
  );
}
