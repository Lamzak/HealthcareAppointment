const API_URL = process.env.NEXT_PUBLIC_API_URL;

export async function fetchClient(endpoint: string, options: RequestInit = {}) {
  const token = localStorage.getItem("token");

  const headers = {
    "Content-Type": "application/json",
    ...options.headers,
    ...(token && { Authorization: `Bearer ${token}` }),
  };

  const response = await fetch(`${API_URL}${endpoint}`, {
    ...options,
    headers,
  });

  // 1. Handle Logout (401)
  if (response.status === 401) {
    if (!window.location.pathname.startsWith("/login")) {
      localStorage.removeItem("token");
      window.location.href = "/login";
    }
  }

  // 2. Handle Empty Success (204)
  if (response.status === 204) return null;

  // 3. Parse Body (Smart: tries JSON, falls back to Text)
  const text = await response.text();
  let data;
  try {
    data = JSON.parse(text); // Try parsing JSON
  } catch {
    data = { message: text }; // If fail, wrap raw text in an object
  }

  // 4. Handle Errors (400, 500, etc)
  if (!response.ok) {
    // Look for 'detail', 'message', 'error', or fallback to raw text
    const errorMessage = data.detail || data.message || data.error || text || "Something went wrong";
    throw new Error(errorMessage);
  }

  return data;
}