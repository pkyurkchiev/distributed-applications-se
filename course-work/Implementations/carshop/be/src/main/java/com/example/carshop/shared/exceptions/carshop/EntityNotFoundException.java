package com.example.carshop.shared.exceptions.carshop;

import lombok.Getter;
import org.springframework.web.bind.annotation.ResponseStatus;

@Getter
@ResponseStatus
public class EntityNotFoundException extends RuntimeException {
    private final int status;

    public EntityNotFoundException(String message, int status) {
        super(message);
        this.status = status;
    }
}
