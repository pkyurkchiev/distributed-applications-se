package com.example.carshop.authentication.dto;

import com.example.carshop.authentication.user.entities.UserType;
import com.example.carshop.shared.annotation.PasswordMatches;
import jakarta.transaction.UserTransaction;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
@PasswordMatches
public class RegisterRequestDTO {

    @NotBlank(message = "Username is required")
    @Size(min = 4, max = 100, message = "Username must be between 4 and 20 characters")
    private String username;

    @NotBlank(message = "Password is required")
    @Pattern(
            regexp = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
            message = "Password must be at least 8 characters long, contain upper and lower case letters, a digit, and a special character"
    )
    private String password;

    @NotBlank(message = "Please confirm your password")
    private String confirmPassword;

    @NotBlank(message = "First name is required")
    private String firstName;

    @NotBlank(message = "Last name is required")
    private String lastName;

    @NotBlank(message = "Email is required")
    @Email(message = "Invalid email format")
    private String emailAddress;

    @NotBlank(message = "Phone number is required")
    @Pattern(regexp = "^\\+?[0-9]{7,15}$", message = "Invalid phone number")
    private String phoneNumber;

}
