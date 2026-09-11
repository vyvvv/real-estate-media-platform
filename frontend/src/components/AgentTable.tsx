import type { Agent } from "../types/Agent";

type AgentTableProps = {
  agents: Agent[];
  onEdit: (agent: Agent) => void;
  onDelete: (id: string) => void;
};

const AgentTable = ({
  agents,
  onEdit,
  onDelete,
}: AgentTableProps) => {
  return (
    <table className="w-full border-collapse text-sm">
      <thead>
        <tr className="border-b border-gray-200 text-left text-xs uppercase tracking-wider text-gray-500">
          <th className="px-4 py-3">Agent</th>
          <th className="px-4 py-3">Company</th>
          <th className="px-4 py-3">Phone</th>
          <th className="px-4 py-3">Email</th>
          <th className="px-4 py-3">Actions</th>
        </tr>
      </thead>

      <tbody>
        {agents.map((agent) => (
          <tr
            key={agent.id}
            className="border-b border-gray-100 hover:bg-gray-50"
          >
            <td className="px-4 py-3">
              <div className="flex items-center gap-3">
                {agent.avatarUrl ? (
                  <img
                    src={agent.avatarUrl}
                    alt={`${agent.agentFirstName} ${agent.agentLastName}`}
                    className="h-10 w-10 rounded-full object-cover"
                  />
                ) : (
                  <div className="flex h-10 w-10 items-center justify-center rounded-full bg-gray-200 text-gray-600">
                    {agent.agentFirstName.charAt(0)}
                    {agent.agentLastName.charAt(0)}
                  </div>
                )}

                <span>
                  {agent.agentFirstName}{" "}
                  {agent.agentLastName}
                </span>
              </div>
            </td>

            <td className="px-4 py-3">
              {agent.companyName}
            </td>

            <td className="px-4 py-3">
              {agent.phoneNumber ?? "—"}
            </td>

            <td className="px-4 py-3">
              {agent.email}
            </td>

            <td className="px-4 py-3">
              <button
                type="button"
                onClick={() => onEdit(agent)}
                className="mr-2 text-xs text-sky-600 hover:text-sky-800"
              >
                Edit
              </button>

              <button
                type="button"
                onClick={() => onDelete(agent.id)}
                className="text-xs text-red-500 hover:text-red-700"
              >
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