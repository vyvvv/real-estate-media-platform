// pages/PropertyDashboard.tsx
import { useParams } from "react-router-dom";
import { Camera, LayoutTemplate, Video, User, Home } from "lucide-react";
import DashboardCard from "../components/DashboardCard";
import LoginNavBar from "../components/LoginNavBar";
import Modal from "../components/Modal";
import PropertyForm from "../components/PropertyForm";
import { useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";





const PropertyDashboard = () => {
 const { id } = useParams();

  const [isOpen, setIsOpen] = useState(false);

  const location = useLocation();
  const property = location.state?.property;
  const navigate = useNavigate()

  const cards = [
  { label: "Photography", icon: <Camera size={50} />, onClick:()=> navigate(`/property/${id}/photography`) },
  { label: "Floor Plan", icon: <LayoutTemplate size={50} /> },
  { label: "Videography", icon: <Video size={50} /> },
  { label: "Agents", icon: <User size={50} /> },
  { label: "Property Details", icon: <Home size={50} />, onClick: () => setIsOpen(true) },
];

return (
  <>
    <LoginNavBar />

    <div className="min-h-screen bg-gray-100 p-20">
      
      {/* 标题置顶居中 */}
      <h1 className="text-2xl font-bold text-center mb-10">Hi, Welcome!</h1>

      {/* 面包屑在左边 */}
      <p className="text-sm text-gray-500 mb-12">
        Property › 170 Russell Street, Melbourne, Victoria, 3000
      </p>

      {/* 六个功能卡片 */}
      <div className="flex justify-center">
        <div className="grid grid-cols-5 gap-20 ">
          {cards.map((card) => (
            <DashboardCard key={card.label} label={card.label} icon={card.icon} onClick={card.onClick} />
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
      subtitle="Please review and complete property details."
      isOpen={isOpen}
      onClose={() => setIsOpen(false)}
      onSave={() => setIsOpen(false)}
    >
      <PropertyForm initialData={property} />
    </Modal>


    


  </>
);
};
export default PropertyDashboard;