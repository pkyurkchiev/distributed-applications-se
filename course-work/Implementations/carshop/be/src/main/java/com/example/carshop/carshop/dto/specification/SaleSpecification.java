package com.example.carshop.carshop.dto.specification;

import com.example.carshop.carshop.entities.impl.CarEntity;
import com.example.carshop.carshop.entities.impl.SaleEntity;
import jakarta.persistence.criteria.CriteriaBuilder;
import jakarta.persistence.criteria.CriteriaQuery;
import jakarta.persistence.criteria.Predicate;
import jakarta.persistence.criteria.Root;
import org.springframework.data.jpa.domain.Specification;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class SaleSpecification implements Specification<SaleEntity> {

    private final Map<String, String> filters;

    public SaleSpecification(Map<String, String> filters) {
        this.filters = filters;
    }

    @Override
    public Predicate toPredicate(Root<SaleEntity> root, CriteriaQuery<?> query, CriteriaBuilder cb) {
        List<Predicate> predicates = new ArrayList<>();

        filters.forEach((key, value) -> {
            if (value == null || value.isEmpty()) {
                return;
            }
            switch (key) {
                case "carMake":
                    predicates.add(cb.equal(root.get("carId").get("make"), value));
                    break;
                case "carModel":
                    predicates.add(cb.equal(root.get("carId").get("model"), value));
                    break;
                case "manufactureYear":
                    predicates.add(cb.equal(root.get("carId").get("manufactureYear"), LocalDate.parse(value)));
                    break;
                case "customerFirstName":
                    predicates.add(cb.equal(root.get("customerId").get("firstName"), value));
                    break;
                case "customerLastName":
                    predicates.add(cb.equal(root.get("customerId").get("lastName"), value));
                    break;
                case "saleDate":
                    predicates.add(cb.equal(root.get("saleDate"), LocalDate.parse(value)));
                    break;
                case "finalPriceFrom":
                    predicates.add(cb.ge(root.get("finalPrice"), new BigDecimal(value)));
                    break;
                case "finalPriceTo":
                    predicates.add(cb.le(root.get("finalPrice"), new BigDecimal(value)));
                    break;
            }
        });

        return cb.and(predicates.toArray(new Predicate[0]));
    }
}
