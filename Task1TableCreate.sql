-- Table: public.tasktable

-- DROP TABLE IF EXISTS public.tasktable;

CREATE TABLE IF NOT EXISTS public.tasktable
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Date" date NOT NULL,
    "LatinSymbols" text COLLATE pg_catalog."default" NOT NULL,
    "RusSymbols" text COLLATE pg_catalog."default" NOT NULL,
    "number" integer NOT NULL,
    "float" numeric NOT NULL,
    CONSTRAINT "TaskTable_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.tasktable
    OWNER to postgres;