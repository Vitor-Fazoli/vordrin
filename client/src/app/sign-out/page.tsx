'use client'

import { getAuth, createUserWithEmailAndPassword  } from 'firebase/auth'
import { useRouter } from 'next/navigation'
import React from 'react'
import { useState } from 'react'

export default function SignOutPage() {
  const router = useRouter()
  const [error, setError] = useState('')

  const handleSignOut = async () => {
    try {
      router.push('/sign-in') // Redirect to sign-in page after successful sign-out
    } catch (err) {
      setError('Failed to sign out. Please try again.')
      console.error(err)
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="p-8 bg-white rounded-lg shadow-md">
        <h1 className="text-2xl font-bold mb-6 text-center">Sign Out</h1>
        {error && (
          <p className="text-red-500 text-sm mb-4 text-center">{error}</p>
        )}
        <form onSubmit={(e) => {
          e.preventDefault()
          handleSignOut()
        }}>
          <button
            type="submit"
            className="w-full bg-red-500 text-white py-2 px-4 rounded hover:bg-red-600 transition-colors"
          >
            Sign Out
          </button>
        </form>
      </div>
    </div>
  )
}
