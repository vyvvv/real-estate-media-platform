import { useCallback, useEffect, useState } from "react";
import LoginNavBar from "../components/LoginNavBar";
import SearchBar from "../components/SearchBar";
import Modal from "../components/Modal";
import AgentForm from "../components/AgentForm";
import AgentTable from "../components/AgentTable";
import {
  createAgent,
  deleteAgent,
  getAgents,
  updateAgent,
} from "../api/agentApi";
import type {
  Agent,
  CreateAgentRequest,
  UpdateAgentRequest,
} from "../types/Agent";

function validateAgent(agent: CreateAgentRequest | UpdateAgentRequest) {
  if (!agent.agentFirstName.trim()) {
    return "First name is required";
  }

  if (!agent.agentLastName.trim()) {
    return "Last name is required";
  }

  if (!agent.companyName.trim()) {
    return "Company name is required";
  }

  if (!agent.email.trim()) {
    return "Email is required";
  }

  return null;
}

function AgentPage() {
  const [agents, setAgents] = useState<Agent[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const [isCreateOpen, setIsCreateOpen] = useState(false);
  const [newAgent, setNewAgent] = useState<CreateAgentRequest | null>(null);

  const [isEditOpen, setIsEditOpen] = useState(false);
  const [editingAgent, setEditingAgent] = useState<Agent | null>(null);
  const [editDraft, setEditDraft] = useState<UpdateAgentRequest | null>(null);

  const loadAgents = useCallback(async () => {
    try {
      setError(null);
      setIsLoading(true);

      const data = await getAgents();
      setAgents(data);
    } catch (loadError) {
      setError(
        loadError instanceof Error
          ? loadError.message
          : "Failed to load agents",
      );
    } finally {
      setIsLoading(false);
    }
  }, []);

  useEffect(() => {
    let cancelled = false;

    getAgents()
      .then((data) => {
        if (!cancelled) {
          setAgents(data);
        }
      })
      .catch((loadError: unknown) => {
        if (!cancelled) {
          setError(
            loadError instanceof Error
              ? loadError.message
              : "Failed to load agents",
          );
        }
      })
      .finally(() => {
        if (!cancelled) {
          setIsLoading(false);
        }
      });

    return () => {
      cancelled = true;
    };
  }, []);

  const closeCreateModal = () => {
    if (isSaving) {
      return;
    }

    setIsCreateOpen(false);
    setNewAgent(null);
  };

  const closeEditModal = () => {
    if (isSaving) {
      return;
    }

    setIsEditOpen(false);
    setEditingAgent(null);
    setEditDraft(null);
  };

  const handleCreate = async () => {
    if (!newAgent) {
      setError("Please complete the agent form.");
      return;
    }

    const validationError = validateAgent(newAgent);

    if (validationError) {
      setError(validationError);
      return;
    }

    try {
      setError(null);
      setIsSaving(true);

      await createAgent(newAgent);
      await loadAgents();

      setNewAgent(null);
      setIsCreateOpen(false);
    } catch (createError) {
      setError(
        createError instanceof Error
          ? createError.message
          : "Failed to create agent",
      );
    } finally {
      setIsSaving(false);
    }
  };

  const openEditModal = (agent: Agent) => {
    setError(null);
    setEditingAgent(agent);
    setEditDraft({
      agentFirstName: agent.agentFirstName,
      agentLastName: agent.agentLastName,
      avatarUrl: agent.avatarUrl,
      companyName: agent.companyName,
      email: agent.email,
      phoneNumber: agent.phoneNumber,
    });
    setIsEditOpen(true);
  };

  const handleUpdate = async () => {
    if (!editingAgent || !editDraft) {
      setError("No agent is selected for editing.");
      return;
    }

    const validationError = validateAgent(editDraft);

    if (validationError) {
      setError(validationError);
      return;
    }

    try {
      setError(null);
      setIsSaving(true);

      await updateAgent(editingAgent.id, editDraft);
      await loadAgents();

      setIsEditOpen(false);
      setEditingAgent(null);
      setEditDraft(null);
    } catch (updateError) {
      setError(
        updateError instanceof Error
          ? updateError.message
          : "Failed to update agent",
      );
    } finally {
      setIsSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    const confirmed = window.confirm(
      "Are you sure you want to delete this agent?",
    );

    if (!confirmed) {
      return;
    }

    try {
      setError(null);
      await deleteAgent(id);
      await loadAgents();
    } catch (deleteError) {
      setError(
        deleteError instanceof Error
          ? deleteError.message
          : "Failed to delete agent",
      );
    }
  };

  return (
    <div>
      <LoginNavBar />

      <main className="flex min-h-screen flex-col items-center bg-gray-50 px-4 pt-14">
        <div className="mb-8 text-center">
          <h1 className="text-3xl font-bold tracking-tight text-gray-900">
            Hi, Welcome!
          </h1>
        </div>

        <div className="mt-12 w-full">
          <SearchBar
            showCreateButton={true}
            placeholder="Search from your agents..."
            createButtonLabel="+ Create Agent"
            onCreateClick={() => {
              setError(null);
              setNewAgent(null);
              setIsCreateOpen(true);
            }}
          />

          <Modal
            title="Create Agent"
            subtitle="Enter the agent details."
            isOpen={isCreateOpen}
            onClose={closeCreateModal}
            onSave={handleCreate}
          >
            <AgentForm onChange={setNewAgent} />
          </Modal>

          <Modal
            title="Edit Agent"
            subtitle="Update the agent details."
            isOpen={isEditOpen}
            onClose={closeEditModal}
            onSave={handleUpdate}
          >
            <AgentForm
              key={editingAgent?.id}
              initialData={editingAgent}
              onChange={setEditDraft}
            />
          </Modal>
        </div>

        <div className="mt-6 w-full">
          {error && (
            <div
              role="alert"
              className="mb-4 rounded-md border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700"
            >
              {error}
            </div>
          )}

          {isLoading ? (
            <p className="py-8 text-center text-sm text-gray-500">
              Loading agents...
            </p>
          ) : agents.length === 0 ? (
            <p className="py-8 text-center text-sm text-gray-500">
              No agents found.
            </p>
          ) : (
            <AgentTable
              agents={agents}
              onEdit={openEditModal}
              onDelete={handleDelete}
            />
          )}
        </div>
      </main>
    </div>
  );
}

export default AgentPage;
