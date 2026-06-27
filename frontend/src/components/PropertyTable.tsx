import { type Property } from "../types/Property";
import { useState } from "react";
import Modal from "./Modal";
import PropertyForm from "./PropertyForm";
import { useNavigate } from "react-router-dom";

type PropertyTableProps = {
  properties: Property[];
};


const statusMap: Record<number, string> = {
  1: "For Sale",
  2: "For Rent",
  3: "Auction",
};
const typeMap: Record<number, string> = {
  1: "House",
  2: "Townhouse",
  3: "Villa",
  4: "Apartment / Unit",
  5: "Others",
};

const PropertyTable = ({ properties }: PropertyTableProps) => {

  const navigate = useNavigate();
  const [editingProperty, setEditingProperty] = useState<Property | null>(null);
  const [isOpen, setIsOpen] = useState(false);
  const handleClose = () => {
    setIsOpen(false);
    setEditingProperty(null); // 关闭时清空
  };

  return (
    <>
    <table className="w-full text-sm border-collapse">
      <thead>
        <tr className="border-b border-gray-200 text-left text-xs text-gray-500 uppercase tracking-wider">
          <th className="py-3 px-4">Property#</th>
          <th className="py-3 px-4">Property Type</th>
          <th className="py-3 px-4">Property Address</th>
          <th className="py-3 px-4">Created At</th>
          <th className="py-3 px-4">Status</th>
          <th className="py-3 px-4">Actions</th>
        </tr>
      </thead>
      <tbody>
        {properties.map((property) => (
          <tr
            key={property.id}
            className="border-b border-gray-100 hover:bg-gray-50"
          >
            <td className="py-3 px-4">#{property.id}</td>
            <td className="py-3 px-4">
              {typeMap[property.propertyType] ?? "Unknown"}
            </td>
            <td className="py-3 px-4">
              {`${property.street}, ${property.city}, ${property.state}, ${property.postcode}`}
            </td>
            <td className="py-3 px-4">
              {property.createdAt.toLocaleDateString()}
            </td>
            <td className="py-3 px-4">
              <span className="bg-yellow-100 text-yellow-700 text-xs px-2 py-1 rounded-full">
                {statusMap[property.listingCaseStatus] ?? "Unknown"}
              </span>
            </td>
            <td className="py-3 px-4">
              <button className="text-sky-600 hover:text-sky-800 text-xs mr-2"  
              onClick={() => { navigate(`/property/${property.id}`, {state:{property}})}}>
                    {/* // setEditingProperty(property); // 保存当前 agent
                    // setIsOpen(true);   */}
                Edit
              </button>
              <button className="text-red-500 hover:text-red-700 text-xs">
                Delete
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>

    <Modal
         title="Property Details"
          subtitle="Please take a moment to review and complete property details."
          isOpen={isOpen}
          onClose={() => setIsOpen(false)} 
          saveLabel = ""
        onSave={() => {
          // 后续接入 API 时在这里处理
          handleClose();
        
        }}
      >
        <PropertyForm initialData={editingProperty} />
      </Modal>
      </>
    
    
  );
};

export default PropertyTable;
