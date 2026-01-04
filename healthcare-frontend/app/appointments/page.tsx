"use client";

import { useEffect, useState } from "react";
import { fetchClient } from "@/utils/api";
import Link from "next/link";
import { useRouter } from "next/navigation";

interface Appointment {
  id: string;
  doctorName: string;
  appointmentTime: string;
  isCancelled: boolean;
}

export default function Dashboard() {
  const [appointments, setAppointments] = useState<Appointment[]>([]);
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  // Load appointments on page load
  useEffect(() => {
    loadAppointments();
  }, []);

  const loadAppointments = async () => {
    try {
      const data = await fetchClient("/Appointments");
      setAppointments(data);
    } catch (error) {
      console.error(error);
      // If error (like 401), fetchClient handles redirect to login
    } finally {
      setLoading(false);
    }
  };

  const handleCancel = async (id: string) => {
    if (!confirm("Are you sure you want to cancel?")) return;
    
    try {
      await fetchClient(`/Appointments/${id}`, { method: "DELETE" });
      // Refresh list
      loadAppointments();
    } catch (error) {
      alert("Failed to cancel");
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    router.push("/login");
  };

  if (loading) return <div className="p-10">Loading...</div>;

  return (
    <div className="min-h-screen bg-gray-50 p-8">
      <div className="max-w-4xl mx-auto">
        {/* Header */}
        <div className="flex justify-between items-center mb-8">
          <h1 className="text-3xl font-bold text-gray-800">My Appointments</h1>
          <div className="space-x-4">
             <Link 
              href="/appointments/book"
              className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition"
            >
              + Book New (AI)
            </Link>
            <button 
              onClick={handleLogout}
              className="bg-gray-200 text-gray-700 px-4 py-2 rounded hover:bg-gray-300"
            >
              Logout
            </button>
          </div>
        </div>

        {/* List */}
        <div className="bg-white rounded-lg shadow overflow-hidden">
          {appointments.length === 0 ? (
            <div className="p-8 text-center text-gray-500">
              You have no upcoming appointments.
            </div>
          ) : (
            <table className="w-full text-left">
              <thead className="bg-gray-100 border-b">
                <tr>
                  <th className="p-4 font-semibold text-gray-600">Doctor</th>
                  <th className="p-4 font-semibold text-gray-600">Date & Time</th>
                  <th className="p-4 font-semibold text-gray-600">Status</th>
                  <th className="p-4 font-semibold text-gray-600">Action</th>
                </tr>
              </thead>
              <tbody>
                {appointments.map((appt) => (
                  <tr key={appt.id} className="border-b hover:bg-gray-50 text-black">
                    <td className="p-4 font-medium">{appt.doctorName}</td>
                    <td className="p-4">
                      {new Date(appt.appointmentTime).toLocaleString()}
                    </td>
                    <td className="p-4">
                      {appt.isCancelled ? (
                        <span className="text-red-500 bg-red-100 px-2 py-1 rounded text-xs">Cancelled</span>
                      ) : (
                        <span className="text-green-500 bg-green-100 px-2 py-1 rounded text-xs">Active</span>
                      )}
                    </td>
                    <td className="p-4">
                      {!appt.isCancelled && (
                        <button
                          onClick={() => handleCancel(appt.id)}
                          className="text-red-600 hover:text-red-800 text-sm font-semibold"
                        >
                          Cancel
                        </button>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </div>
      </div>
    </div>
  );
}