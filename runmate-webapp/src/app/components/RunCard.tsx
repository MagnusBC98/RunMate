import Link from "next/link";

type Run = {
  id: string;
  runDate: string;
  distanceInKm: number;
  avgPaceInMinutesPerKm: string;
};

type RunCardProps = {
  run: Run;
};

export default function RunCard({ run }: RunCardProps) {
  return (
    <div className="mb-4">
      <Link href={`/runs/${run.id}/details`}>
        <div className="block rounded-lg bg-gray-800 p-4 transition hover:bg-gray-700">
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
      </Link>
    </div>
  );
}
