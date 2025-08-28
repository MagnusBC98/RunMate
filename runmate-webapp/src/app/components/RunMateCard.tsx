"use client";

import Link from "next/link";
import { useState } from "react";
import { RunMate } from "../types/runmate";

type RunMateCardProps = {
  mate: RunMate;
  onUnmatch: (requestId: string) => Promise<boolean>;
};

export default function RunMateCard({ mate, onUnmatch }: RunMateCardProps) {
  const [isLoading, setIsLoading] = useState(false);

  const handleUnmatchClick = async () => {
    setIsLoading(true);
    await onUnmatch(mate.requestId);
    setIsLoading(false);
  };

  return (
    <div className="rounded-lg bg-gray-800 p-4">
      <div className="flex items-center justify-between">
        <div>
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
        <button
          onClick={handleUnmatchClick}
          disabled={isLoading}
          className="rounded bg-red-600 px-4 py-2 text-sm font-bold text-white transition hover:bg-red-700 disabled:opacity-50"
        >
          {isLoading ? "..." : "Unmatch"}
        </button>
      </div>
    </div>
  );
}
