import { useState } from "react";

const AgentForm = () => {

const [firstName, setFirstName] = useState("");
const [lastName, setLastName] = useState("");
const [phone, setPhone] = useState("");
const [email, setEmail] = useState("");
const [companyName, setCompanyName] = useState("");
const [, setProfileURL] = useState<File | null>(null);
const [preview,SetPreview] = useState("");

const handleProfileURL = (e: React.ChangeEvent<HTMLInputElement>) =>{
    
    const file  = e.target.files?.[0];
    if (!file) return;

    setProfileURL(file);
    SetPreview(URL.createObjectURL(file))
}
  return (
    <div className="space-y-4">
      <div>
        <label htmlFor="firstName" className="block text-sm font-medium text-gray-700 mb-1">
          First Name
        </label>
        <input
            id="firstName"
            name="firstName"
            type="text"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            placeholder="Enter first name"
            autoComplete="given-name"
            required
            className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div>
        <label htmlFor="lastName" className="block text-sm font-medium text-gray-700 mb-1">
          Last Name
        </label>
        <input
        id = "lastName"
        name = "lastName"
        value = {lastName}
        onChange ={(lastname) => setLastName(lastname.target.value)}
        placeholder="Please enter last name"
        autoComplete = "family-name"
        required
          type="text"
          className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"

        />
      </div>

      <div>
        <label htmlFor="companyName" className="block text-sm font-medium text-gray-700 mb-1">
          Company Name
        </label>
        <input
        id = "companyName"
        name = "companyName"
        value = {companyName}
        type="text"
        onChange = {(companyname) => setCompanyName(companyname.target.value)}
        autoComplete = "organization"
        required
        placeholder="Please entry your company's name"
          className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div>
        <label htmlFor = "email" className="block text-sm font-medium text-gray-700 mb-1">
          Email 
        </label>
        <input
        id = "email"
        name = "email"
        value = {email}
        onChange = {(email) => setEmail(email.target.value)}
        autoComplete = "email"
        required
        placeholder="Please enter your email"
          type="email"
          className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div>
        <label htmlFor = "phone" className="block text-sm font-medium text-gray-700 mb-1">
          Phone Number
        </label>
        <input
        id = "phone"
          type="tel"
          name = "phone"
          value = {phone}
          onChange={ (phone)=>setPhone(phone.target.value)}
          autoComplete ="tel"
          required
          placeholder="Please enter your contact number"
          className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div>
        <label htmlFor = "profileURL" className="block text-sm font-medium text-gray-700 mb-1">
          Images
        </label>
        <input
            id="profileURL"
            name="profileURL"
            type="file"
            accept="image/*"
            onChange={handleProfileURL}
            className="hidden"
        />

        <label
            htmlFor="profileURL"
            className="inline-flex cursor-pointer items-center rounded-md border border-gray-300 px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50"
        >
            Upload Image
        </label>

        {preview && (
            <img
            src={preview}
            alt="Profile preview"
            className="mt-3 h-20 w-20 rounded-full object-cover"
            />
        )}
        </div>


      
      
    </div>
  );
};

export default AgentForm;
