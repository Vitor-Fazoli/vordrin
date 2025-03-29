import { NextResponse } from 'next/server'
import type { NextRequest } from 'next/server'

export function middleware(request: NextRequest) {
  return NextResponse.redirect(new URL('/sign-in', request.url))
}

export const config = {
  matcher: ['/main', '/main/:path*'], // Include both `/main` and its subpaths
}