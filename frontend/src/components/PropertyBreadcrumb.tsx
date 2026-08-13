import { Link } from "react-router-dom";
import type { Property } from "../types/Property";

type PropertyBreadcrumbProps = {
  property: Property | null;
  currentPage?: string;
};

const PropertyBreadcrumb = ({
  property,
  currentPage,
}: PropertyBreadcrumbProps) => {
  const address = property
    ? `${property.street}, ${property.city}, ${property.state}, ${property.postcode}`
    : "Loading property...";

  return (
    <nav
      aria-label="Breadcrumb"
      className="mb-12 flex items-center text-sm text-gray-500"
    >
      <Link
        to="/dashboard"
        className="font-medium text-sky-600 hover:text-sky-800 hover:underline"
      >
        Property
      </Link>

      <span className="mx-2" aria-hidden="true">
        ›
      </span>

      {currentPage && property ? (
        <Link
          to={`/property/${property.id}`}
          className="text-sky-600 hover:text-sky-800 hover:underline"
        >
          {address}
        </Link>
      ) : (
        <span>{address}</span>
      )}

      {currentPage && (
        <>
          <span className="mx-2" aria-hidden="true">
            ›
          </span>

          <span aria-current="page">
            {currentPage}
          </span>
        </>
      )}
    </nav>
  );
};

export default PropertyBreadcrumb;