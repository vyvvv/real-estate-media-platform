import { useEffect, useState } from "react";
import type { ListingCase } from "../types/listing";
import { getListings } from "../api/listingApi";

function ListingPage() {
  const [listings, setListings] = useState<ListingCase[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    async function loadListings() {
      try {
        const data = await getListings();
        setListings(data);
      } catch (err) {
        setError("Failed to load listings.");
      } finally {
        setLoading(false);
      }
    }

    loadListings();
  }, []);

  if (loading) {
    return <p>Loading listings...</p>;
  }

  if (error) {
    return <p>{error}</p>;
  }

  return (
    <section>
      <h2>Property List</h2>

      {listings.map((listing) => (
        <div key={listing.id} className="listing-card">
          <h3>{listing.title}</h3>

          <p>
            Address: {listing.street}, {listing.city}, {listing.state}{" "}
            {listing.postcode}
          </p>

          <p>Description: {listing.description}</p>
          <p>Price: ${listing.price.toLocaleString()}</p>

          <p>
            {listing.bedrooms} bedrooms · {listing.bathrooms} bathrooms ·{" "}
            {listing.garages} garages
          </p>

          <p>Floor Area: {listing.floorArea} m²</p>
          <p>Status Code: {listing.listingCaseStatus}</p>
        </div>
      ))}
    </section>
  );
}

export default ListingPage;