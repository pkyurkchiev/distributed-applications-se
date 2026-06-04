import { useState, useEffect, useRef } from "react";
import { fetchUsersByUsername } from "../../http requests/users";
import { addUserToProject } from "../../http requests/projects";
import { useParams } from "react-router-dom";

export default function UserSearchCombobox({ handleAddUserToProject }) {
  const params = useParams();
  const [userSearchQuery, setUserSearchQuery] = useState("");
  const [searchResults, setSearchResults] = useState([]);
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const [selectedUser, setSelectedUser] = useState(null);
  const [errorMessage, setErrorMessage] = useState("");

  const comboboxRef = useRef(null);

  useEffect(() => {
    if (userSearchQuery.trim().length >= 3) {
      async function performSearch() {
        try {
          const results = await fetchUsersByUsername(userSearchQuery);
          setSearchResults(results);
          setIsDropdownOpen(true);
          setErrorMessage("");
        } catch (error) {
          console.error("Failed to fetch users:", error);
        }
      }

      performSearch();
    } else {
      setSearchResults([]);
      setIsDropdownOpen(false);
    }
  }, [userSearchQuery]);

  useEffect(() => {
    function handleClickOutside(event) {
      if (comboboxRef.current && !comboboxRef.current.contains(event.target)) {
        setIsDropdownOpen(false);
      }
    }
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  function handleSelectUser(clickedUser) {
    setSelectedUser(clickedUser);
    setUserSearchQuery(clickedUser.username);
    setIsDropdownOpen(false);
    setErrorMessage("");
  }

  function handleClear() {
    setUserSearchQuery("");
    setSelectedUser(null);
    setSearchResults([]);
    setErrorMessage("");
  }

  async function handleAddClick() {
    if (!selectedUser) {
      setErrorMessage(
        "Please select a valid user from the dropdown before adding.",
      );
      return;
    }

    const response = await handleAddUserToProject(selectedUser);

    if (response.status === 400) {
      setErrorMessage(
        response.response?.data?.message || "Could not add user.",
      );
    }

    if (response.status === 200) {
      setSelectedUser(response.data.message);

      handleClear();
    }
  }

  return (
    <section className="mb-8 max-w-md">
      <label className="block text-sm font-bold text-stone-700 mb-1">
        Add Team Member (Max 10)
      </label>

      <div className="relative" ref={comboboxRef}>
        <div className="flex gap-2 items-center">
          <div className="relative flex-1 flex items-center">
            <input
              type="text"
              placeholder="Type 3+ characters to search..."
              value={userSearchQuery}
              onFocus={() =>
                userSearchQuery.trim().length >= 3 && setIsDropdownOpen(true)
              }
              onChange={(e) => {
                setUserSearchQuery(e.target.value);
                if (selectedUser && e.target.value !== selectedUser.username) {
                  setSelectedUser(null);
                }
              }}
              className="w-full p-2.5 bg-white border border-stone-300 rounded shadow-sm text-stone-800 pr-10 focus:outline-none focus:ring-2 focus:ring-stone-400"
            />

            <div className="absolute right-3 flex items-center pointer-events-none text-stone-400">
              {userSearchQuery ? (
                <button
                  type="button"
                  className="pointer-events-auto hover:text-stone-600 font-bold text-xs"
                  onClick={handleClear}
                >
                  ✕
                </button>
              ) : (
                <span className="text-xs">▼</span>
              )}
            </div>
          </div>

          <button
            type="button"
            onClick={handleAddClick}
            className="px-4 py-2.5 bg-stone-700 text-white font-medium rounded shadow-sm hover:bg-stone-800 transition-colors"
          >
            Add
          </button>
        </div>

        {isDropdownOpen && searchResults.length > 0 && (
          <ul className="absolute z-50 left-0 right-16 mt-1 bg-white border border-stone-200 rounded shadow-lg max-h-56 overflow-y-auto">
            {searchResults.map((u) => (
              <li
                key={u.id}
                onClick={() => handleSelectUser(u)}
                className="p-3 border-b border-stone-100 last:border-b-0 hover:bg-stone-100 cursor-pointer text-stone-700 transition-colors duration-150 flex flex-col"
              >
                <span className="font-medium text-stone-900">{u.username}</span>
                <span className="text-xs text-stone-400">{u.email}</span>
              </li>
            ))}
          </ul>
        )}

        {isDropdownOpen &&
          searchResults.length === 0 &&
          userSearchQuery.trim().length >= 3 && (
            <div className="absolute z-50 left-0 right-16 mt-1 p-3 bg-white border border-stone-200 rounded shadow-lg text-stone-500 text-sm">
              No users found matching "{userSearchQuery}"
            </div>
          )}
      </div>

      {selectedUser && (
        <p className="text-xs text-green-600 mt-1.5 font-medium">
          Ready to add user: {selectedUser.username}
        </p>
      )}

      {errorMessage && (
        <p className="text-xs text-red-500 mt-1.5 font-medium">
          {errorMessage}
        </p>
      )}
    </section>
  );
}
