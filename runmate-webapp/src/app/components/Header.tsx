"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";
import { useAuth } from "../context/AuthContext";
import Logo from "./Logo";

export default function Header() {
  const { isLoggedIn, logout } = useAuth();
  const router = useRouter();

  const handleLogout = () => {
    logout();
    router.push("/");
  };

  return (
    <header className="flex w-full items-center justify-between bg-gray-800 p-4 text-white">
      <Link href={isLoggedIn ? "/dashboard" : "/"}>
        <Logo size="small" />
      </Link>

      <nav className="flex items-center space-x-4">
        <Link
          href="/about"
          className="px-3 py-2 text-gray-300 transition hover:text-white"
        >
          About
        </Link>
        {isLoggedIn ? (
          <>
            <Link
              href="/profile/me"
              className="px-3 py-2 text-gray-300 transition hover:text-white"
            >
              My Profile
            </Link>
            <button
              onClick={handleLogout}
              className="rounded bg-red-600 px-4 py-2 font-bold transition hover:bg-red-700"
            >
              Logout
            </button>
          </>
        ) : (
          <>
            <Link
              href="/login"
              className="px-3 py-2 text-gray-300 transition hover:text-white"
            >
              Login
            </Link>
            <Link href="/register">
              <button className="rounded bg-blue-600 px-4 py-2 font-bold transition hover:bg-blue-700">
                Register
              </button>
            </Link>
          </>
        )}
      </nav>
    </header>
  );
}
