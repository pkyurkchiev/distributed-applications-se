package com.example.carshop.carshop.dto;

import jakarta.validation.constraints.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class CarDTO {
    private Long id;

    @NotBlank(message = "Make is required")
    @Size(max = 50, message = "Make must be at most 50 characters")
    private String make;

    @NotBlank(message = "Model is required")
    @Size(max = 50, message = "Model must be at most 50 characters")
    private String model;

    @NotNull(message = "Manufacture year is required")
    @PastOrPresent(message = "Manufacture year must be in the past or present")
    private LocalDate manufactureYear;

    @NotNull(message = "Price is required")
    @DecimalMin(value = "0.0", inclusive = false, message = "Price must be greater than 0")
    private BigDecimal price;

    private boolean isInStock;

    @Size(max = 10, message = "You can upload at most 10 images")
    private List<
            @NotBlank(message = "Image data must not be blank")
            @Pattern(regexp = "^[A-Za-z0-9+/=]+$", message = "Invalid Base64 format")
                    String> imagesBase64;
}