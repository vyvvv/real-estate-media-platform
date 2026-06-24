import { type Agent } from "../types/Agent";
import { useState } from "react";
import AgentForm from "./AgentForm";
import Modal from "./Modal";

type AgentProp = {
  agents: Agent[];
};

const AgentTable = ({ agents }: AgentProp) => {
   const [editingAgent, setEditingAgent] = useState<Agent | null>(null);
   const [isOpen, setIsOpen] = useState(false);

    const handleClose = () => {
    setIsOpen(false);
    setEditingAgent(null); // 关闭时清空
  };

  return (
    <>
    <table className="w-full text-sm border-collapse">
      <thead>
        <tr className="border-b border-gray-200 text-left text-xs text-gray-500 uppercase tracking-wider">
          <th className="py-3 px-4">Agent Name#</th>
          <th className="py-3 px-4">Company Name </th>
          <th className="py-3 px-4">Contact Number </th>
          <th className="py-3 px-4">Contact Email</th>
          <th className="py-3 px-4">Actions</th>
        </tr>
      </thead>
      <tbody>
        {agents.map((agent) => (
          <tr
            key={agent.contactId}
            className="border-b border-gray-100 hover:bg-gray-50"
          >
            <td className="py-3 px-4">
              {`${agent.firstName} ${agent.lastName}`}
            </td>
            <td className="py-3 px-4">{agent.companyName}</td>
            <td className="py-3 px-4">{agent.phoneNumber}</td>
            <td className="py-3 px-4">{agent.email}</td>

            <td className="py-3 px-4">
             <button
                  className="text-sky-600 hover:text-sky-800 text-xs mr-2"
                  onClick={() => {
                    setEditingAgent(agent); // 保存当前 agent
                    setIsOpen(true);        // 打开 Modal
                  }}
                >
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
        title="Agent Details"
        subtitle="Please review and complete agent details."
        isOpen={isOpen}
        onClose={handleClose}
        onSave={() => {
          // 后续接入 API 时在这里处理
          handleClose();
        }}
      >
        <AgentForm initialData={editingAgent} />
      </Modal>
      </>
  );
};

export default AgentTable;
