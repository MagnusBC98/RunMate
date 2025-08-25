import Link from "next/link";
import Logo from "./components/Logo";

export default function HomePage() {
  return (
    <main className="flex min-h-screen flex-col items-center justify-center bg-gray-900 text-white">
      <div className="mb-12">
        <Logo size="large" />
      </div>

      <div className="flex space-x-4">
        <Link href="/register">
          <button className="rounded bg-blue-600 px-6 py-3 font-bold text-white transition hover:bg-blue-700">
            Register
          </button>
        </Link>
        <Link href="/login">
          <button className="rounded bg-gray-700 px-6 py-3 font-bold text-white transition hover:bg-gray-600">
            Login
          </button>
        </Link>
        <Link href="/about">
          <button className="rounded bg-gray-700 px-6 py-3 font-bold text-white transition hover:bg-gray-600">
            About
          </button>
        </Link>
      </div>
    </main>
  );
}
