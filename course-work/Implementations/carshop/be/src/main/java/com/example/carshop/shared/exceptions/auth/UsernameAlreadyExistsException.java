package com.example.carshop.shared.exceptions.auth;

import lombok.Getter;
import org.springframework.web.bind.annotation.ResponseStatus;

@Getter
@ResponseStatus
public class UsernameAlreadyExistsException extends RuntimeException {
    private final int status;

    public UsernameAlreadyExistsException(String message, int status) {
        super(message);
        this.status = status;
    }
}
