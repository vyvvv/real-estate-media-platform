import { useState } from "react";

const PropertyForm = () => {
  const [status, setStatus] = useState("For Sale");
  const [propertyType, setPropertyType] = useState("");
  const [bed, setBed] = useState(0);
  const [bath, setBath] = useState(0);
  const [car, setCar] = useState(0);
  const [area, setArea] = useState(0);

  return (
    <div className="space-y-4">
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Property Title
        </label>
        <input
          type="text"
          className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          Property Status
        </label>
        <div className="flex gap-4">
          {["For Sale", "Auction", "For Rent"].map((s) => (
            <label key={s} className="flex items-center gap-1 text-sm">
              <input
                type="radio"
                name="status"
                value={s}
                checked={status === s}
                onChange={() => setStatus(s)}
              />
              {s}
            </label>
          ))}
        </div>
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          Property Type
        </label>
        <div className="flex flex-wrap gap-4">
          {["House", "Town House", "Apartment/Units", "Villa", "Others"].map(
            (t) => (
              <label key={t} className="flex items-center gap-1 text-sm">
                <input
                  type="radio"
                  name="propertyType"
                  value={t}
                  checked={propertyType === t}
                  onChange={() => setPropertyType(t)}
                />
                {t}
              </label>
            ),
          )}
        </div>
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Search Address
        </label>
        <input
          type="text"
          placeholder="Search..."
          autoComplete="street-address"
          className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
        />
      </div>

      <div className="grid grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            City
          </label>
          <input
            type="text"
            autoComplete="address-level2"
            className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            State
          </label>
          <input
            type="text"
             autoComplete="address-level1"
            className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
          />
        </div>
      </div>

      <div className="grid grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Postcode
          </label>
          <input
            type="text"
             autoComplete="postal-code"
            className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
          />
        </div>
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Price
          </label>
          <input
            type="number"
            className="w-full border border-gray-300 rounded-md px-3 py-2 text-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
          />
        </div>
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">
          Basic Information
        </label>
        <div className="flex flex-wrap gap-6">
          {[
            { label: "Bed", value: bed, setValue: setBed },
            { label: "Bath", value: bath, setValue: setBath },
            { label: "Car", value: car, setValue: setCar },
          ].map(({ label, value, setValue }) => (
            <div
              key={label}
              className="flex items-center gap-2 border border-gray-300 rounded-md px-3 py-2"
            >
              <span className="text-sm text-gray-600">{label}</span>
              <button
                type="button"
                onClick={() => setValue(Math.max(0, value - 1))}
                className="text-gray-500 hover:text-gray-700"
              >
                -
              </button>
              <span className="text-sm w-4 text-center">{value}</span>
              <button
                type="button"
                onClick={() => setValue(value + 1)}
                className="text-gray-500 hover:text-gray-700"
              >
                +
              </button>
            </div>
          ))}

          <div className="flex items-center gap-2 border border-gray-300 rounded-md px-3 py-2">
            <span className="text-sm text-gray-600">Area</span>
            <input
              type="number"
              value={area}
              onChange={(e) => setArea(Number(e.target.value))}
              className="w-12 text-sm text-center outline-none"
            />
            <span className="text-sm text-gray-500">m2</span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PropertyForm;
