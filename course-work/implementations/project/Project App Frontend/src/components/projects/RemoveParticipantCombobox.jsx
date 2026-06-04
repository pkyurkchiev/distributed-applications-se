import { useState } from "react";

export default function RemoveParticipantCombobox({
  participants = [],
  handleRemoveUserFromProject,
}) {
  const [selectedUserId, setSelectedUserId] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  async function handleRemoveClick() {
    setErrorMessage("");
    setSuccessMessage("");

    if (!selectedUserId) {
      setErrorMessage(
        "Please select a team member from the dropdown to remove.",
      );
      return;
    }

    const userToRemove = participants.find(
      (u) => u.id === parseInt(selectedUserId),
    );

    const response = await handleRemoveUserFromProject(userToRemove);

    if (response.status === 400) {
      setErrorMessage(
        response.response?.data?.message || "Could not remove user.",
      );
    }

    if (response.status === 200) {
      setSuccessMessage(
        response.data.message ||
          `Successfully removed ${userToRemove.username}.`,
      );
      setSelectedUserId(""); // Reset dropdown state
    }
  }

  const selectColorClass =
    selectedUserId === ""
      ? "text-stone-400" // Matches your input placeholder text color
      : "text-stone-800";

  if (participants.length === 0) {
    return (
      <div className="mb-8">
        <section className=" max-w-md p-4 border border-dashed border-stone-300 rounded bg-stone-50">
          <p className="text-xs text-stone-500 italic text-center">
            No team members added to this project yet.
          </p>
        </section>
        {successMessage && (
          <p className="text-xs text-green-600 mt-1.5 font-medium">
            {successMessage}
          </p>
        )}
      </div>
    );
  }

  return (
    <section className="mb-8 max-w-md">
      <label className="block text-sm font-bold text-stone-700 mb-1">
        Remove Team Member
      </label>

      <div className="flex gap-2 items-center">
        <select
          value={selectedUserId}
          onChange={(e) => {
            setSelectedUserId(e.target.value);
            setErrorMessage("");
            setSuccessMessage("");
          }}
          className={`flex-1 p-2.5 bg-white border border-stone-300 rounded shadow-sm text-sm focus:outline-none focus:ring-2 focus:ring-stone-400 transition-colors ${selectColorClass}`}
        >
          <option value="">-- Choose member to remove --</option>
          {participants.map((u) => (
            <option key={u.id} value={u.id}>
              {u.username}
            </option>
          ))}
        </select>

        <button
          type="button"
          onClick={handleRemoveClick}
          className="px-4 py-2.5 bg-red-700 text-white font-medium rounded shadow-sm hover:bg-red-800 transition-colors text-sm"
        >
          Remove
        </button>
      </div>

      {successMessage && (
        <p className="text-xs text-green-600 mt-1.5 font-medium">
          {successMessage}
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
