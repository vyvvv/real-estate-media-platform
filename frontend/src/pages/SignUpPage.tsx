import LoginNavBar from "../components/LoginNavBar";


const SignUpPage=()=> {
    
  return (
   <div>
        <LoginNavBar />
        
      <main className="min-h-screen bg-gray-50 flex flex-col items-center pt-14 px-4">
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold tracking-tight text-gray-900">
            Sign Up Photography Company
          </h1>

        <p className="mt-2 text-sm text-gray-600">
          Already an account?{" "}
          <a
            href="/login" 
            className="font-medium text-blue-600 hover:text-blue-500"
          >
            Sign In
          </a>
        </p>
      </div>

      <form className="w-full max-w-md rounded-md bg-white p-10 shadow-sm ring-1 ring-gray-200">
        <div className="space-y-6">

            <div>
            <label
              htmlFor="companyName"
              className="block text-sm font-medium text-gray-900"
            >
              Company Name
            </label>

            <input
              type="text"
              id="companyName"
              name="companyName"
              required
              className="mt-2 block w-full rounded-md border border-gray-300 px-3 py-2 text-sm text-gray-900 shadow-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <div>
            <label
              htmlFor="email"
              className="block text-sm font-medium text-gray-900"
            >
              Email address
            </label>

            <input
              type="email"
              id="email"
              name="email"
              required
              className="mt-2 block w-full rounded-md border border-gray-300 px-3 py-2 text-sm text-gray-900 shadow-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <div>
            <label
              htmlFor="phone"
              className="block text-sm font-medium text-gray-900"
            >
              Phone Number
            </label>

            <input
              type="tel"
              id="phone"
              name="phone"
              required
              className="mt-2 block w-full rounded-md border border-gray-300 px-3 py-2 text-sm text-gray-900 shadow-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <div>
            <label
              htmlFor="password"
              className="block text-sm font-medium text-gray-900"
            >
              Password
            </label>

            <input
              type="password"
              id="password"
              name="password"
              required
              className="mt-2 block w-full rounded-md border border-gray-300 px-3 py-2 text-sm text-gray-900 shadow-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <div>
            <label
              htmlFor="confirmPassword"
              className="block text-sm font-medium text-gray-900"
            >
              Confirm Password
            </label>

            <input
              type="password"
              id="confirmPassword"
              name="confirmPassword"
              required
              className="mt-2 block w-full rounded-md border border-gray-300 px-3 py-2 text-sm text-gray-900 shadow-sm outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>
          

          <div className="flex items-center justify-between">
            <div className="flex items-center">
              <input
                type="checkbox"
                id="remember-me"
                name="remember-me"
                className="h-4 w-4 rounded border-gray-300 text-blue-600 focus:ring-blue-500"
              />

              <label
                htmlFor="remember-me"
                className="ml-2 block text-sm text-gray-900"
              >
                I agree to the <a href= '' className="underline">Term and Conditions</a>
              </label>
            </div>

           
          </div>

          <button
            type="submit"
            className="w-full rounded-md bg-blue-600 px-4 py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-blue-500 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
          >
            Sign Up
          </button>
        </div>
      </form>
    </main>
    </div>
  );
}

export default SignUpPage;