"use client";

import Link from "next/link";
import { useState } from "react";

type Run = {
  id: string;
  runDate: string;
  distanceInKm: number;
  avgPaceInMinutesPerKm: string;
  userFirstName?: string;
  userLastName?: string;
  userId?: string;
};

type RunCardProps = {
  run: Run;
  isClickable?: boolean;
  onMakeRequest?: (runId: string) => void;
};

export default function RunCard({
  run,
  isClickable = false,
  onMakeRequest,
}: RunCardProps) {
  const [isLoading, setIsLoading] = useState(false);

  const handleRequestClick = async () => {
    if (onMakeRequest) {
      setIsLoading(true);
      await onMakeRequest(run.id);
      setIsLoading(false);
    }
  };

  const cardVisualContent = (
    <div
      className={`block rounded-lg bg-gray-800 p-4 ${
        isClickable ? "transition hover:bg-gray-700" : ""
      }`}
    >
      <div className="mb-3">
        <p className="font-bold text-lg">
          {new Date(run.runDate).toLocaleDateString("en-GB", {
            year: "numeric",
            month: "long",
            day: "numeric",
          })}
        </p>
        <div className="mt-2 flex justify-between text-gray-300">
          <span>Distance: {run.distanceInKm.toFixed(2)} km</span>
          <span>Pace: {run.avgPaceInMinutesPerKm} /km</span>
        </div>
      </div>

      {(run.userFirstName && run.userId) || onMakeRequest ? (
        <div className="flex items-center justify-between border-t border-gray-700 pt-3">
          {run.userFirstName && run.userId ? (
            <div>
              <span className="text-sm text-gray-400">Posted by: </span>
              <Link
                href={`/profile/${run.userId}`}
                className="text-sm font-bold text-blue-400 hover:underline"
              >
                {run.userFirstName} {run.userLastName}
              </Link>
            </div>
          ) : (
            <div />
          )}{" "}
          {onMakeRequest && (
            <button
              onClick={handleRequestClick}
              disabled={isLoading}
              className="rounded bg-green-600 px-4 py-2 text-sm font-bold text-white transition hover:bg-green-700 disabled:opacity-50"
            >
              {isLoading ? "Sending..." : "Make Request"}
            </button>
          )}
        </div>
      ) : null}
    </div>
  );

  return (
    <div className="mb-4">
      {" "}
      {isClickable ? (
        <Link href={`/runs/${run.id}/details`}>{cardVisualContent}</Link>
      ) : (
        cardVisualContent
      )}
    </div>
  );
}
