import Link from "next/link";

export default function AboutPage() {
  return (
    <main className="flex min-h-screen flex-col items-center bg-gray-900 p-8 text-white">
      <div className="w-full max-w-2xl">
        <h1 className="mb-6 text-center text-4xl font-bold text-blue-400">
          About RunMate
        </h1>
        <p className="mb-4 text-lg text-gray-300">
          RunMate is an app designed to match up runners of similar levels and
          goals, allowing them to connect and run together. We believe that
          running with others can be a powerful motivator and a great way to
          stay safe and consistent.
        </p>
        <p className="mb-4 text-lg text-gray-300">
          Running clubs can sometimes be intimidating for newcomers. RunMate
          aims to level the playing field, removing the fear of "what if I’m not
          fast enough?" and allowing you to enjoy social runs with people who
          are looking for the same thing.
        </p>
        <div className="mt-8 text-center">
          <Link href="/">
            <button className="rounded bg-gray-700 px-6 py-3 font-bold text-white transition hover:bg-gray-600">
              Back to Home
            </button>
          </Link>
        </div>
      </div>
    </main>
  );
}
