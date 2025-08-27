"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";

export default function SearchRunsPage() {
  const [minDistance, setMinDistance] = useState("");
  const [maxDistance, setMaxDistance] = useState("");
  const [minPace, setMinPace] = useState("");
  const [maxPace, setMaxPace] = useState("");

  const router = useRouter();

  const handleSearch = (event: React.FormEvent) => {
    event.preventDefault();

    const query = new URLSearchParams({
      minDistance: minDistance,
      maxDistance: maxDistance,
      minPace: minPace,
      maxPace: maxPace,
    }).toString();

    router.push(`/runs/search-results?${query}`);
  };

  return (
    <main className="flex flex-col items-center p-8">
      <div className="w-full max-w-lg">
        <h1 className="mb-6 text-3xl font-bold">Search for a Run</h1>
        <form
          onSubmit={handleSearch}
          className="rounded-lg bg-gray-800 p-8 shadow-md"
        >
          <div className="mb-4 grid grid-cols-1 gap-4 sm:grid-cols-2">
            <div>
              <label
                className="mb-2 block text-sm font-bold text-gray-300"
                htmlFor="minDistance"
              >
                Min Distance (km)
              </label>
              <input
                className="input-field"
                id="minDistance"
                type="number"
                step="0.1"
                placeholder="e.g., 5"
                value={minDistance}
                onChange={(e) => setMinDistance(e.target.value)}
              />
            </div>
            <div>
              <label
                className="mb-2 block text-sm font-bold text-gray-300"
                htmlFor="maxDistance"
              >
                Max Distance (km)
              </label>
              <input
                className="input-field"
                id="maxDistance"
                type="number"
                step="0.1"
                placeholder="e.g., 10"
                value={maxDistance}
                onChange={(e) => setMaxDistance(e.target.value)}
              />
            </div>
          </div>

          <div className="mb-6 grid grid-cols-1 gap-4 sm:grid-cols-2">
            <div>
              <label
                className="mb-2 block text-sm font-bold text-gray-300"
                htmlFor="minPace"
              >
                Min Pace (per km)
              </label>
              <input
                className="input-field"
                id="minPace"
                type="text"
                placeholder="hh:mm:ss"
                value={minPace}
                onChange={(e) => setMinPace(e.target.value)}
              />
            </div>
            <div>
              <label
                className="mb-2 block text-sm font-bold text-gray-300"
                htmlFor="maxPace"
              >
                Max Pace (per km)
              </label>
              <input
                className="input-field"
                id="maxPace"
                type="text"
                placeholder="hh:mm:ss"
                value={maxPace}
                onChange={(e) => setMaxPace(e.target.value)}
              />
            </div>
          </div>

          <button
            className="w-full rounded bg-blue-600 px-4 py-3 font-bold text-white transition hover:bg-blue-700 focus:outline-none"
            type="submit"
          >
            Search
          </button>
        </form>
      </div>
    </main>
  );
}
