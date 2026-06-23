import { useState } from "react";
import LoginNavBar from "../components/LoginNavBar";
import SearchBar from "../components/SearchBar";
import Modal from "../components/Modal";
import AgentForm from "../components/AgentForm";
import AgentTable from "../components/AgentTable";
import { mockAgent } from "../data/mockAgent";


function AgentPage() {

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
        <SearchBar  showCreateButton={true}  placeholder="Search from your agents..." createButtonLabel="+ Create Agent"   onCreateClick={() => setIsOpen(true)} />
          <Modal
            title="Agent Details"
            subtitle="Please review and complete agent details."
            isOpen={isOpen}
            onClose={() => setIsOpen(false)} // 关闭 Modal
            onSave={() => {
              // 后续接入 API 时在这里处理
              setIsOpen(false);
            }}
          >
          <AgentForm />
          </Modal>

         </div>

          <div className="mt-6 w-full">
          <AgentTable agents={mockAgent}/></div>


      </main>
    </div>


  );
}

export default AgentPage;