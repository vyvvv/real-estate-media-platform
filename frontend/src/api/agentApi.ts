import type {
  Agent,
  CreateAgentRequest,
  UpdateAgentRequest,
} from "../types/Agent";

const API_BASE_URL = "http://localhost:5166";

export async function getAgents(): Promise<Agent[]> {
  const response = await fetch(
    `${API_BASE_URL}/api/Agents`,
  );

  if (!response.ok) {
    throw new Error("Failed to fetch agents");
  }

  return response.json();
}

export async function createAgent(
  data: CreateAgentRequest,
) {
  const response = await fetch(
    `${API_BASE_URL}/api/Agents`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    },
  );

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to create agent");
  }

  return response.json();
}

export async function updateAgent(
  id: string,
  data: UpdateAgentRequest,
) {
  const response = await fetch(
    `${API_BASE_URL}/api/Agents/${id}`,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    },
  );

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to update agent");
  }

  return response.json();
}

export async function deleteAgent(id: string) {
  const response = await fetch(
    `${API_BASE_URL}/api/Agents/${id}`,
    {
      method: "DELETE",
    },
  );

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Failed to delete agent");
  }

  return response.json();
}