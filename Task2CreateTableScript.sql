-- Table: taskschem.balance

-- DROP TABLE IF EXISTS taskschem.balance;

CREATE TABLE IF NOT EXISTS taskschem.balance
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    account_number integer NOT NULL,
    ib_assets money,
    ib_passive money,
    debit money,
    credit money,
    CONSTRAINT input_balance_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS taskschem.balance
    OWNER to postgres;