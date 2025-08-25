"use client";

import { useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { jwtDecode } from "jwt-decode";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);
  const router = useRouter();

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setError(null);

    if (!email || !password) {
      setError("Both fields are required.");
      return;
    }

    try {
      const response = await fetch("https://localhost:7251/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      if (!response.ok) {
        throw new Error("Login failed. Please check your credentials.");
      }

      const data = await response.json();
      const decodedToken: { sub: string } = jwtDecode(data.token);
      localStorage.setItem("userId", decodedToken.sub);

      localStorage.setItem("authToken", data.token);

      router.push("/dashboard");
    } catch (err: any) {
      setError(err.message || "An unexpected error occurred.");
    }
  };

  return (
    <main className="flex min-h-screen flex-col items-center justify-center bg-gray-900 text-white">
      <div className="w-full max-w-xs rounded bg-gray-800 p-8 shadow-md">
        <h1 className="mb-6 text-center text-2xl font-bold">
          Login to RunMate
        </h1>
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="mb-2 block text-sm font-bold" htmlFor="email">
              Email
            </label>
            <input
              className="input-field"
              id="email"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>

          <div className="mb-6">
            <label className="mb-2 block text-sm font-bold" htmlFor="password">
              Password
            </label>
            <input
              className="input-field"
              id="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>

          {error && <p className="mb-4 text-center text-red-500">{error}</p>}

          <div className="flex flex-col items-center">
            <button
              className="focus:shadow-outline w-full rounded bg-blue-600 px-4 py-2 font-bold text-white transition hover:bg-blue-700 focus:outline-none"
              type="submit"
            >
              Sign In
            </button>
            <Link
              href="/register"
              className="mt-4 inline-block align-baseline text-sm text-blue-400 hover:text-blue-500"
            >
              Need an account? Register
            </Link>
          </div>
        </form>
      </div>
    </main>
  );
}
