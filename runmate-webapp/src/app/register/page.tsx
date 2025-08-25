"use client";

import { useState } from "react";
import Link from "next/link";

export default function RegisterPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setError(null);
    setSuccess(null);

    if (!firstName || !lastName || !email || !password) {
      setError("All fields are required.");
      return;
    }
    if (password.length < 6) {
      setError("Password must be at least 6 characters long.");
      return;
    }

    try {
      const response = await fetch("https://localhost:7251/api/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password, firstName, lastName }),
      });

      if (!response.ok) {
        const errorData = await response.json();
        const message = errorData.title || "Registration failed.";
        throw new Error(message);
      }

      setSuccess("Registration successful! Please log in.");
    } catch (err: any) {
      setError(err.message || "An unexpected error occurred.");
    }
  };

  return (
    <main className="flex min-h-screen flex-col items-center justify-center bg-gray-900 text-white">
      <div className="w-full max-w-md rounded bg-gray-800 p-8 shadow-md">
        <h1 className="mb-6 text-center text-2xl font-bold">
          Create Your Account
        </h1>
        <form onSubmit={handleSubmit}>
          {/* First Name & Last Name */}
          <div className="mb-4 grid grid-cols-1 gap-4 md:grid-cols-2">
            <div>
              <label
                className="mb-2 block text-sm font-bold"
                htmlFor="firstName"
              >
                First Name
              </label>
              <input
                className="input-field"
                id="firstName"
                type="text"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
              />
            </div>
            <div>
              <label
                className="mb-2 block text-sm font-bold"
                htmlFor="lastName"
              >
                Last Name
              </label>
              <input
                className="input-field"
                id="lastName"
                type="text"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
              />
            </div>
          </div>

          {/* Email -- This div needs a margin-bottom class */}
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

          {/* Password -- This div needs a larger margin-bottom */}
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

          {/* Error & Success Messages */}
          {error && <p className="mb-4 text-center text-red-500">{error}</p>}
          {success && (
            <p className="mb-4 text-center text-green-500">{success}</p>
          )}

          {/* Buttons */}
          <div className="flex flex-col items-center">
            <button
              className="focus:shadow-outline w-full rounded bg-blue-600 px-4 py-2 font-bold text-white transition hover:bg-blue-700 focus:outline-none"
              type="submit"
            >
              Register
            </button>
            <Link
              href="/login"
              className="mt-4 inline-block align-baseline text-sm text-blue-400 hover:text-blue-500"
            >
              Already have an account? Login
            </Link>
          </div>
        </form>
      </div>
    </main>
  );
}
