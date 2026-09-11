import { useEffect, useState } from "react";
import type { AgentFormProps } from "../types/Agent";

const AgentForm = ({ initialData = null, onChange }: AgentFormProps) => {
  const [firstName, setFirstName] = useState(
    initialData?.agentFirstName ?? "",
  );
  const [lastName, setLastName] = useState(
    initialData?.agentLastName ?? "",
  );
  const [companyName, setCompanyName] = useState(
    initialData?.companyName ?? "",
  );
  const [email, setEmail] = useState(initialData?.email ?? "");
  const [phone, setPhone] = useState(initialData?.phoneNumber ?? "");
  const [avatarUrl, setAvatarUrl] = useState(
    initialData?.avatarUrl ?? "",
  );

  useEffect(() => {
    onChange?.({
      agentFirstName: firstName,
      agentLastName: lastName,
      avatarUrl: avatarUrl || null,
      companyName,
      email,
      phoneNumber: phone || null,
    });
  }, [firstName, lastName, avatarUrl, companyName, email, phone, onChange]);

  const handleAvatarFile = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];

    if (!file) {
      return;
    }

    const objectUrl = URL.createObjectURL(file);
    setAvatarUrl(objectUrl);
  };

  return (
    <div className="space-y-4">
      <div>
        <label
          htmlFor="firstName"
          className="mb-1 block text-sm font-medium text-gray-700"
        >
          First Name
        </label>
        <input
          id="firstName"
          name="firstName"
          type="text"
          value={firstName}
          onChange={(event) => setFirstName(event.target.value)}
          placeholder="Enter first name"
          autoComplete="given-name"
          required
          className="w-full rounded-md border border-gray-300 px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div>
        <label
          htmlFor="lastName"
          className="mb-1 block text-sm font-medium text-gray-700"
        >
          Last Name
        </label>
        <input
          id="lastName"
          name="lastName"
          type="text"
          value={lastName}
          onChange={(event) => setLastName(event.target.value)}
          placeholder="Enter last name"
          autoComplete="family-name"
          required
          className="w-full rounded-md border border-gray-300 px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div>
        <label
          htmlFor="companyName"
          className="mb-1 block text-sm font-medium text-gray-700"
        >
          Company Name
        </label>
        <input
          id="companyName"
          name="companyName"
          type="text"
          value={companyName}
          onChange={(event) => setCompanyName(event.target.value)}
          autoComplete="organization"
          required
          placeholder="Enter company name"
          className="w-full rounded-md border border-gray-300 px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div>
        <label
          htmlFor="email"
          className="mb-1 block text-sm font-medium text-gray-700"
        >
          Email
        </label>
        <input
          id="email"
          name="email"
          type="email"
          value={email}
          onChange={(event) => setEmail(event.target.value)}
          autoComplete="email"
          required
          placeholder="Enter email"
          className="w-full rounded-md border border-gray-300 px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div>
        <label
          htmlFor="phone"
          className="mb-1 block text-sm font-medium text-gray-700"
        >
          Phone Number
        </label>
        <input
          id="phone"
          name="phone"
          type="tel"
          value={phone}
          onChange={(event) => setPhone(event.target.value)}
          autoComplete="tel"
          placeholder="Enter phone number"
          className="w-full rounded-md border border-gray-300 px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div>
        <label className="mb-1 block text-sm font-medium text-gray-700">
          Avatar
        </label>
        <input
          id="avatarFile"
          name="avatarFile"
          type="file"
          accept="image/*"
          onChange={handleAvatarFile}
          className="hidden"
        />

        <label
          htmlFor="avatarFile"
          className="inline-flex cursor-pointer items-center rounded-md border border-gray-300 px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50"
        >
          Upload Image
        </label>

        {avatarUrl && (
          <img
            src={avatarUrl}
            alt="Agent avatar preview"
            className="mt-3 h-20 w-20 rounded-full object-cover"
          />
        )}
      </div>
    </div>
  );
};

export default AgentForm;
