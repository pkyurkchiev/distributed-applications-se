package com.example.carshop.shared.exceptions;

import com.example.carshop.shared.exceptions.auth.InvalidUsernameOrPasswordException;
import com.example.carshop.shared.exceptions.carshop.EntityNotFoundException;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

import java.util.HashMap;
import java.util.Map;

@ControllerAdvice
public class RestExceptionHandler {

    @ExceptionHandler(InvalidUsernameOrPasswordException.class)
    public ResponseEntity<Map<String, String>> handleCustomException(InvalidUsernameOrPasswordException ex) {
        Map<String, String> body = new HashMap<>();
        body.put("error", ex.getMessage());

        return ResponseEntity.status(ex.getStatus()).body(body);
    }

    @ExceptionHandler(EntityNotFoundException.class)
    public ResponseEntity<Map<String, String>> handleEntityNotFound(EntityNotFoundException ex) {
        Map<String, String> body = new HashMap<>();
        body.put("error", ex.getMessage());

        return ResponseEntity.status(ex.getStatus()).body(body);
    }

    @ExceptionHandler(Exception.class)
    public ResponseEntity<Map<String, Object>> handleGeneralError(Exception ex) {

        System.out.println(ex.getMessage());
        Map<String, Object> body = new HashMap<>();
        body.put("error", "Internal server error");
        body.put("details", ex.getMessage());

        return ResponseEntity.status(500).body(body);
    }
}
