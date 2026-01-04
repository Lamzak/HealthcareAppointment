"use client";

import { useState } from "react";
import { fetchClient } from "@/utils/api";
import { useRouter } from "next/navigation";

interface Doctor {
  id: string;
  name: string;
  specialization: string;
  pricePerVisit: number;
}

export default function BookPage() {
  const router = useRouter();
  
  // Steps: 0 = Input Symptoms, 1 = Select Doctor
  const [step, setStep] = useState(0);
  
  const [symptoms, setSymptoms] = useState("");
  const [recommendedDoctors, setRecommendedDoctors] = useState<Doctor[]>([]);
  const [selectedDoctor, setSelectedDoctor] = useState<Doctor | null>(null);
  const [date, setDate] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  // 1. Ask AI for Doctors
  const handleGetRecommendation = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError("");
    
    try {
      // Sending raw string in body, need to match Backend "FromBody string"
      const data = await fetchClient("/Doctors/recommend", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ symptoms }), // Wrap in object if backend expects object, or just string if configured that way
      });
      
      setRecommendedDoctors(data);
      setStep(1);
    } catch (err: any) {
      setError("Failed to get recommendations. Try again.");
    } finally {
      setLoading(false);
    }
  };

  // 2. Book the Appointment
  const handleBook = async () => {
    if (!selectedDoctor || !date) return;

    try {
      await fetchClient("/Appointments", {
        method: "POST",
        body: JSON.stringify({
          doctorId: selectedDoctor.id,
          time: new Date(date).toISOString(),
          symptoms: symptoms // Optional: save symptoms if you added that property
        }),
      });
      
      // Success -> Redirect to Dashboard
      router.push("/appointments");
    } catch (err: any) {
      setError(err.message || "Booking failed");
    }
  };

  return (
    <div className="min-h-screen bg-gray-50 flex items-center justify-center p-4">
      <div className="w-full max-w-2xl bg-white rounded-lg shadow-lg p-8">
        <h2 className="text-2xl font-bold mb-6 text-gray-800">
          AI Doctor Recommendation
        </h2>

        {error && <div className="mb-4 bg-red-100 text-red-700 p-3 rounded">{error}</div>}

        {/* STEP 1: SYMPTOMS */}
        {step === 0 && (
          <form onSubmit={handleGetRecommendation} className="space-y-6">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-2">
                Describe your symptoms
              </label>
              <textarea
                required
                rows={4}
                className="w-full p-3 border rounded focus:ring-2 focus:ring-blue-500 text-black"
                placeholder="e.g., I have a sharp pain in my chest and difficulty breathing..."
                value={symptoms}
                onChange={(e) => setSymptoms(e.target.value)}
              />
            </div>
            <button
              type="submit"
              disabled={loading}
              className="w-full bg-blue-600 text-white py-3 rounded font-semibold hover:bg-blue-700 disabled:bg-blue-300"
            >
              {loading ? "Analyzing with AI..." : "Get Recommendation"}
            </button>
          </form>
        )}

        {/* STEP 2: SELECT DOCTOR & TIME */}
        {step === 1 && (
          <div className="space-y-6">
            <div>
              <h3 className="text-lg font-medium text-gray-700 mb-3">Recommended Doctors:</h3>
              <div className="grid gap-4">
                {recommendedDoctors.map((doc) => (
                  <div 
                    key={doc.id}
                    onClick={() => setSelectedDoctor(doc)}
                    className={`p-4 border rounded cursor-pointer transition ${
                      selectedDoctor?.id === doc.id 
                        ? "border-blue-500 bg-blue-50 ring-2 ring-blue-200" 
                        : "hover:bg-gray-50 border-gray-200"
                    }`}
                  >
                    <div className="font-bold text-gray-900">{doc.name}</div>
                    <div className="text-sm text-gray-600">{doc.specialization} • ${doc.pricePerVisit}</div>
                  </div>
                ))}
              </div>
            </div>

            {selectedDoctor && (
              <div>
                 <label className="block text-sm font-medium text-gray-700 mb-2">
                    Select Date & Time
                 </label>
                 <input 
                   type="datetime-local"
                   required
                   className="w-full p-2 border rounded text-black"
                   onChange={(e) => setDate(e.target.value)}
                 />
              </div>
            )}

            <div className="flex gap-4 mt-6">
              <button
                onClick={() => setStep(0)}
                className="w-1/3 bg-gray-200 text-gray-700 py-3 rounded font-semibold"
              >
                Back
              </button>
              <button
                onClick={handleBook}
                disabled={!selectedDoctor || !date}
                className="w-2/3 bg-green-600 text-white py-3 rounded font-semibold hover:bg-green-700 disabled:bg-green-300"
              >
                Confirm Booking
              </button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}