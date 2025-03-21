'use client'
import { useState } from 'react';

export default function RegisterPage() {
  const [formData, setFormData] = useState({
    username: "",
    password: ""
  });

  return (
    <div className="min-w-full h-full flex items-center justify-center">
      <div className="p-8 ring-2 ring-rose-700 rounded-lg shadow-md">
        <h1 className="text-2xl font-bold mb-6 text-center text-rose-700">Register</h1>
        <form >
          <div className="mb-4">
            <label className="block text-rose-700 text-sm font-bold mb-2">
              Username
            </label>
            <input
              type="text"
              value={formData.username}
              name='username'
              onChange={(e) => setFormData({ ...formData, username: e.target.value })}
              className="w-full px-3 py-2 border border-rose-700 rounded focus:outline-none focus:ring-2 focus:ring-rose-700"
              required
            />
          </div>
          <div className="mb-6">
            <label className="block text-rose-700 text-sm font-bold mb-2">
              Password
            </label>
            <input
              type="password"
              value={formData.password}
              name='password'
              onChange={(e) => setFormData({ ...formData, password: e.target.value })}
              className="w-full px-3 py-2 border border-rose-700 rounded focus:outline-none focus:ring-2 focus:ring-rose-700"
              required
            />
          </div>
          <button
            type="submit"
            className="w-full bg-rose-700 text-white py-2 px-4 rounded hover:bg-rose-800 transition-colors cursor-pointer"
          >
            Join
          </button>
        </form>
      </div>
    </div>
  )
}
