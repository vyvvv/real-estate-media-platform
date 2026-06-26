// components/DashboardCard.tsx
type DashboardCardProps = {
  label: string;
  icon: React.ReactNode;
  onClick?: () => void;
  isActive?: boolean;
};

const DashboardCard = ({ label, icon, onClick, isActive }: DashboardCardProps) => {
  return (
    <button
      onClick={onClick}
      className={`flex flex-col items-center justify-center gap-2 p-6 rounded-lg border text-sm font-medium transition
        ${isActive ? "bg-blue-50 border-blue-300 text-blue-600" : "bg-gray-100 border-gray-200 text-gray-600 hover:bg-gray-400"}`}
    >
      {icon}
      {label}
    </button>
  );
};

export default DashboardCard;