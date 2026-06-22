import { useState } from "react";
import LoginNavBar from "../components/LoginNavBar";
import SearchBar from "../components/SearchBar";
import PropertyForm from "../components/PropertyForm";
import Modal from "../components/Modal";

const AdminDashboardPage: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);

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
            onClose={() => setIsOpen(false)} // 关闭 Modal
            onSave={() => {
              // 后续接入 API 时在这里处理
              setIsOpen(false);
            }}
          >
          <PropertyForm />
          </Modal>

        </div>
      </main>
    </div>
  );
};

export default AdminDashboardPage;
