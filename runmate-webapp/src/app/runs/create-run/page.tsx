"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";

export default function CreateRunPage() {
  const [runDate, setRunDate] = useState<string>(
    new Date().toISOString().substring(0, 10)
  );
  const [distanceInKm, setDistanceInKm] = useState<string>("");
  const [avgPace, setAvgPace] = useState<string>("");
  const [error, setError] = useState<string | null>(null);

  const router = useRouter();

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setError(null);

    const token = localStorage.getItem("authToken");
    if (!token) {
      router.push("/login");
      return;
    }

    if (!runDate || !distanceInKm || !avgPace) {
      setError("All fields are required.");
      return;
    }

    try {
      const response = await fetch("https://localhost:7251/api/runs", {
        method: "POST",
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

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.title || "Failed to create run.");
      }

      router.push("/runs/my-runs");
    } catch (err: any) {
      setError(err.message);
    }
  };

  return (
    <main className="flex flex-col items-center p-8">
      <div className="w-full max-w-lg">
        <h1 className="mb-6 text-3xl font-bold">Create a New Run</h1>
        <form
          onSubmit={handleSubmit}
          className="rounded-lg bg-gray-800 p-8 shadow-md"
        >
          <div className="mb-4">
            <label
              className="mb-2 block text-sm font-bold text-gray-300"
              htmlFor="runDate"
            >
              Run Date
            </label>
            <input
              className="input-field"
              id="runDate"
              type="date"
              value={runDate}
              onChange={(e) => setRunDate(e.target.value)}
            />
          </div>

          <div className="mb-4">
            <label
              className="mb-2 block text-sm font-bold text-gray-300"
              htmlFor="distance"
            >
              Distance (km)
            </label>
            <input
              className="input-field"
              id="distance"
              type="number"
              step="0.01"
              placeholder="e.g., 5.5"
              value={distanceInKm}
              onChange={(e) => setDistanceInKm(e.target.value)}
            />
          </div>

          {/* Average Pace */}
          <div className="mb-6">
            <label
              className="mb-2 block text-sm font-bold text-gray-300"
              htmlFor="pace"
            >
              Average Pace (per km)
            </label>
            <input
              className="input-field"
              id="pace"
              type="text"
              placeholder="hh:mm:ss"
              value={avgPace}
              onChange={(e) => setAvgPace(e.target.value)}
            />
          </div>

          {error && <p className="mb-4 text-center text-red-500">{error}</p>}

          <button
            className="w-full rounded bg-blue-600 px-4 py-3 font-bold text-white transition hover:bg-blue-700 focus:outline-none"
            type="submit"
          >
            Save Run
          </button>
        </form>
      </div>
    </main>
  );
}
