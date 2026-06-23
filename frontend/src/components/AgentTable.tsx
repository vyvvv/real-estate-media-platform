import { type Agent } from "../types/Agent";

type AgentProp = {
    agents: Agent[];
};

const AgentTable = ({ agents }: AgentProp) =>{

return (
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
            <td className="py-3 px-4">
              {agent.email}
            </td>
         
            <td className="py-3 px-4">
              <button className="text-sky-600 hover:text-sky-800 text-xs mr-2">
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
  );
};

export default AgentTable;
