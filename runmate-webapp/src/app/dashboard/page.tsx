"use client";

import { useEffect, useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";

type UserProfile = {
  firstName: string;
  lastName: string;
};

export default function DashboardPage() {
  const [user, setUser] = useState<UserProfile | null>(null);
  const [error, setError] = useState<string | null>(null);
  const router = useRouter();

  useEffect(() => {
    const fetchUserProfile = async () => {
      const token = localStorage.getItem("authToken");
      const userId = localStorage.getItem("userId");

      if (!token) {
        router.push("/login");
        return;
      }

      try {
        const response = await fetch(
          `https://localhost:7251/api/users/${userId}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (!response.ok) {
          throw new Error("Failed to fetch user profile.");
        }

        const data: UserProfile = await response.json();
        setUser(data);
      } catch (err: any) {
        setError(err.message);
        router.push("/login");
      }
    };

    fetchUserProfile();
  }, [router]);

  if (error) return <p className="text-red-500">{error}</p>;
  if (!user) return <p>Loading...</p>;

  return (
    <main className="flex min-h-screen flex-col items-center bg-gray-900 p-8 text-white">
      <div className="w-full max-w-4xl">
        <h1 className="mb-8 text-4xl font-bold">Hello, {user.firstName}!</h1>

        <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
          <Link
            href="/runs/create-run"
            className="nav-button bg-gray-700 hover:bg-gray-600"
          >
            Create Run
          </Link>

          <Link
            href="/runs/my-runs"
            className="nav-button bg-gray-700 hover:bg-gray-600"
          >
            My Runs
          </Link>

          <Link
            href="/runs/search-runs"
            className="nav-button bg-gray-700 hover:bg-gray-600"
          >
            Search Runs
          </Link>

          <Link
            href="/runmates"
            className="nav-button bg-gray-700 hover:bg-gray-600"
          >
            My RunMates
          </Link>
        </div>
      </div>
    </main>
  );
}
