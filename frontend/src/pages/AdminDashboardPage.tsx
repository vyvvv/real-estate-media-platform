import { useState,useEffect } from "react";
import LoginNavBar from "../components/LoginNavBar";
import SearchBar from "../components/SearchBar";
import PropertyForm from "../components/PropertyForm";
import Modal from "../components/Modal";
import PropertyTable from "../components/PropertyTable";
//import { mockProperties } from "../data/mockProperty";
import { createListing, deleteListing, getListings } from "../api/listingApi";


import type { Property } from "../types/Property";
import type { CreateListingRequest } from "../types/ListingCase";


const AdminDashboardPage: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [properties, setProperties] = useState<Property[]>([]);
const [newListing, setNewListing] = useState<CreateListingRequest | null>(null);

const loadListings = async () => {
  const data = await getListings();

  setProperties(
    data.map((item) => ({
      ...item,
      createdAt: new Date(item.createdAt),
    }))
  );
};

useEffect(() => {
  let cancelled = false;

  async function initialLoad() {
    const data = await getListings();

    if (cancelled) return;

    setProperties(
      data.map((item) => ({
        ...item,
        createdAt: new Date(item.createdAt),
      })),
    );
  }

  initialLoad();

  return () => {
    cancelled = true;
  };
}, []);

  return (
    <div>
      <LoginNavBar />

      <main className="min-h-screen bg-gray-50 flex flex-col items-center pt-14 px-4">
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold tracking-tight text-gray-900">
            Hi, Welcome!
          </h1>
        </div>

        <div className="mt-12 w-full">
          <SearchBar
            showCreateButton={true}
            placeholder="Search from your listing case"
            onCreateClick={() => setIsOpen(true)}
          />

<Modal
  title="Property Details"
  subtitle="Please take a moment to review and complete property details."
  isOpen={isOpen}
  onClose={() => setIsOpen(false)}
  onSave={async () => {
    if (!newListing) {
      return;
    }
     if (!newListing.title.trim() && typeof newListing.title !== "string") {
    alert("Title is required");
    return;
  }

  if (!newListing.street.trim()  && typeof newListing.title !== "string") {
    alert("Street is required");
    return;
  }

  if (!newListing.city.trim()  && typeof newListing.title !== "string") {
    alert("City is required");
    return;
  }

  if (!newListing.price && newListing.price <= 0) {
    alert("Price must be greater than 0");
    return;
  }


    await createListing(newListing);
    await loadListings();

    setNewListing(null);
    setIsOpen(false);
  }}
>
  <PropertyForm onChange={setNewListing} />
</Modal>

        </div>
        <div className="mt-6 w-full">
         <PropertyTable
            properties={properties}
            onDelete={async (id) => {
              const confirmed = window.confirm("Are you sure you want to delete this listing?");

              if (!confirmed) {
                return;
  }
              await deleteListing(id);
              await loadListings();
            }}
/>
</div>

      </main>
    </div>
  );
};

export default AdminDashboardPage;
