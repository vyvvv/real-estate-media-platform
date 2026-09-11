export type Agent = {
  id: string;
  agentFirstName: string;
  agentLastName: string;
  avatarUrl: string | null;
  companyName: string;
  email: string;
  phoneNumber: string | null;
};

export type CreateAgentRequest = {
  agentFirstName: string;
  agentLastName: string;
  avatarUrl: string | null;
  companyName: string;
  email: string;
  phoneNumber: string | null;
};

export type UpdateAgentRequest = CreateAgentRequest;

export interface AgentFormProps {
  initialData?: Agent | null;
  onChange?: (
    data: CreateAgentRequest | UpdateAgentRequest,
  ) => void;
}