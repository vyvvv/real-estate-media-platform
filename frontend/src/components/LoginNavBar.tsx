function LoginNavBar() {
  return (
    <nav className="w-full shadow-sm bg-sky-600">
      <div className="max-auto flex h-16 items-center justify-between px-10">
        <div className="flex item-center gap-16">
          <a href="#" className="text-xl font-bold tracking-tight text-white">
            Real Estate System
          </a>{" "}
          <ul className="flex item-center">
            <li>
              {" "}
              <a
                href="#"
                className="text-white text-sm font-semibold hover:text-blue-200"
              >
                Listing Cases
              </a>
            </li>
          </ul>
        </div>

        <button
          type="button"
          className="rounded-md p-2 text-white hover:bg-blue-700"
          aria-label="Logout"
        >
          <span className="text-xl">⇱</span>
        </button>
      </div>
    </nav>
  );
}

export default LoginNavBar;

// function LoginNavBar() {
//   return (
//     <nav className="bg-white shadow">
//       <ul>
//         <li>
//           <a href="#" className="">
//             Listing Cases
//           </a>
//         </li>
//       </ul>
//     </nav>
//   );
// }

// export default LoginNavBar;
