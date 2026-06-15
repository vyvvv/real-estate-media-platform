import type { ListingCase } from "../types/listing";

const API_BASE_URL = "http://localhost:5166";

export async function getListings(): Promise<ListingCase[]> {
  const response = await fetch(`${API_BASE_URL}/api/Listings`);

  if (!response.ok) {
    throw new Error("Failed to fetch listings");
  }

  return response.json();
}