// pages/PropertyDashboard.tsx
import { useParams, useNavigate } from "react-router-dom";
import { Camera, LayoutTemplate, Video, User, Home } from "lucide-react";
import DashboardCard from "../components/DashboardCard";
import LoginNavBar from "../components/LoginNavBar";
import Modal from "../components/Modal";
import PropertyForm from "../components/PropertyForm";
import { useEffect, useState } from "react";
import type { Property } from "../types/Property";
import type { UpdateListingRequest  } from "../types/ListingCase";
import { getListingById, updateListing } from "../api/listingApi";
import PropertyBreadcrumb from "../components/PropertyBreadCrumb";

const PropertyDashboard = () => {
  const { id } = useParams();
  const [property, setProperty] = useState<Property | null>(null);

  const [isOpen, setIsOpen] = useState(false);

  const [editDraft, setEditDraft] = useState<UpdateListingRequest  | null>(null);

  const navigate = useNavigate();

  const loadProperty = async () => {
    if (!id) {
      return;
    }

    const data = await getListingById(Number(id));

    setProperty({
      ...data,
      createdAt: new Date(data.createdAt),
    });
  };

  useEffect(() => {
    if (!id) {
      return;
    }

    async function fetchProperty() {
      const data = await getListingById(Number(id));

      setProperty({
        ...data,
        createdAt: new Date(data.createdAt),
      });
    }

    fetchProperty();
  }, [id]);

  const handleSave = async () => {
    if (!id || !editDraft) {
      return;
    }

    if (!editDraft.title.trim()) {
      alert("Title is required");
      return;
    }

    if (!editDraft.street.trim()) {
      alert("Street is required");
      return;
    }

    if (!editDraft.city.trim()) {
      alert("City is required");
      return;
    }

    if (editDraft.price <= 0) {
      alert("Price must be greater than 0");
      return;
    }

    await updateListing(Number(id), editDraft);

    await loadProperty();

    setEditDraft(null);
    setIsOpen(false);
  };

  const cards = [
    {
      label: "Photography",
      icon: <Camera size={50} />,
      onClick: () => navigate(`/property/${id}/photography`),
    },
    { label: "Floor Plan", icon: <LayoutTemplate size={50} /> },
    { label: "Videography", icon: <Video size={50} /> },
    { label: "Agents", icon: <User size={50} /> },

    {
      label: "Property Details",
      icon: <Home size={50} />,
      onClick: () => {
        if (!property) {
          return;
        }

        setEditDraft({
          title: property.title,
          description: property.description,
          street: property.street,
          city: property.city,
          state: property.state,
          postcode: property.postcode,
          longitude: property.longitude ?? 0,
          latitude: property.latitude ?? 0,
          price: property.price,
          bedrooms: property.bedrooms,
          bathrooms: property.bathrooms,
          garages: property.garages,
          floorArea: property.floorArea,
          propertyType: property.propertyType,
          saleCategory: property.saleCategory,
        });

        setIsOpen(true);
      },
    },
  ];

  return (
    <>
      <LoginNavBar />

      <div className="min-h-screen bg-gray-100 p-20">
        {/* 标题置顶居中 */}
        <h1 className="text-2xl font-bold text-center mb-10">Hi, Welcome!</h1>

        {/* 面包屑在左边 */}
        <PropertyBreadcrumb property={property} />

        {/* 六个功能卡片 */}
        <div className="flex justify-center">
          <div className="grid grid-cols-5 gap-20 ">
            {cards.map((card) => (
              <DashboardCard
                key={card.label}
                label={card.label}
                icon={card.icon}
                onClick={card.onClick}
              />
            ))}
          </div>
        </div>

        {/* 底部按钮居中 */}
        <div className="flex justify-center mt-20">
          <button className="px-8 py-3 bg-sky-600 text-white rounded-full hover:bg-sky-700 text-sm font-medium">
            Deliver to agent
          </button>
        </div>
      </div>

      <Modal
        title="Property Details"
        subtitle="Please review and update property details."
        isOpen={isOpen}
        onClose={() => {
          setIsOpen(false);
          setEditDraft(null);
        }}
        onSave={handleSave}
      >
        <PropertyForm
          key={property?.id}
          initialData={property}
          onChange={setEditDraft}
        />
      </Modal>
    </>
  );
};
export default PropertyDashboard;
