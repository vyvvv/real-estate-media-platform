import type { CreateListingRequest, ListingCase, UpdateListingRequest } from "../types/ListingCase";

const API_BASE_URL = "http://localhost:5166";

export async function getListings(): Promise<ListingCase[]> {
  const response = await fetch(`${API_BASE_URL}/api/Listings`);

  if (!response.ok) {
    throw new Error("Failed to fetch listings");
  }

  return response.json();
}


export async function getListingById(
  id: number,
): Promise<ListingCase> {
  const response = await fetch(
    `${API_BASE_URL}/api/Listings/${id}`,
  );

  if (!response.ok) {
    throw new Error("Failed to fetch listing");
  }

  return response.json();
}


export async function createListing(data: CreateListingRequest) {
  const response = await fetch(`${API_BASE_URL}/api/Listings`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    const errorText = await response.text();
    console.error("Create listing failed:", response.status, errorText);
    throw new Error("Failed to create listing");
  }

  return response.json();
}


export async function updateListing(
  id: number,
  data: UpdateListingRequest,
) {
  const response = await fetch(
    `${API_BASE_URL}/api/Listings/${id}`,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    },
  );

  if (!response.ok) {
    const errorText = await response.text();

    console.error(
      "Update listing failed:",
      response.status,
      errorText,
    );

    throw new Error(errorText || "Failed to update listing");
  }

  return response.json();
}

export async function deleteListing(id: number) {
  const response = await fetch(`${API_BASE_URL}/api/Listings/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error("Failed to delete listing");
  }

  return response.json();
}

export async function updateListingStatus(id: number, listingCaseStatus: number) {
  const response = await fetch(`${API_BASE_URL}/api/Listings/${id}/status`, {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ listingCaseStatus }),
  });

  if (!response.ok) {
    throw new Error("Failed to update listing status");
  }

  return response.json();
}


