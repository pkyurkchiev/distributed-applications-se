package com.example.carshop.carshop.dto.request;

import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.Map;

@Data
@NoArgsConstructor
@AllArgsConstructor
public abstract class AbstractRequestClass {
    @Min(value = 0, message = "Page index must be 0 or greater")
    private int page = 0;

    @Min(value = 1, message = "Page size must be at least 1")
    @Max(value = 100, message = "Page size must be at most 100")
    private int size = 10;

    @NotNull(message = "Sort order (asc) must be specified")
    private Boolean asc = true;

    @NotBlank(message = "Sort by field must be specified")
    private String sortBy = "id";

    private Map<@NotBlank(message = "Filter key must not be blank") String,
            @NotBlank(message = "Filter value must not be blank") String> filters;
}
