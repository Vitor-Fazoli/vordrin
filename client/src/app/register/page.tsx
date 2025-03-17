'use client'

import { getAuth, createUserWithEmailAndPassword } from 'firebase/auth'
import { useRouter } from 'next/navigation'
import { useState } from 'react';
import { app } from '../layout';

export default function RegisterPage() {
  const [formData, setFormData] = useState({
    email: "",
    password: ""
  });

  const router = useRouter()

  const handleRegister = async () => {
    try {
      const auth = getAuth(app)
      await createUserWithEmailAndPassword(auth, formData.email, formData.password)
      router.push('/game/initial')
    } catch (err) {
      window.alert('Failed to create account. Please try again.')
      console.error(err)
    }
  }

  return (
    <div className="min-w-full h-full flex items-center justify-center">
      <div className="p-8 ring-2 ring-rose-700 rounded-lg shadow-md">
        <h1 className="text-2xl font-bold mb-6 text-center text-rose-700">Register</h1>
        <form onSubmit={(e) => {
          e.preventDefault()
          handleRegister()
        }}>
          <div className="mb-4">
            <label className="block text-rose-700 text-sm font-bold mb-2">
              Email
            </label>
            <input
              type="email"
              value={formData.email}
              name='email'
              onChange={(e) => setFormData({ ...formData, email: e.target.value })}
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
